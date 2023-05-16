using AutoMapper;
using FrancaSW.Comun;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceOrdenesProduccion : IServiceOrdenesProduccion
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;

        public ServiceOrdenesProduccion(FrancaSwContext _context, ISecurityService _securityService, IMapper _mapper)
        {
            this.context = _context;
            this.securityService = _securityService;
            this.mapper = _mapper;
        }


        public async Task<List<DtoOrdenProdListado>> GetListadoOrdenProduccion()
        {
            var query = from op in context.OrdenesProducciones
                        join p in context.Productos on op.IdProducto equals p.IdProducto
                        join u in context.Usuarios on op.IdUsuario equals u.IdUsuario
                        join eop in context.EstadosOrdenesProducciones on op.IdEstadoOrdenProduccion equals eop.IdEstadoOrdenProduccion
                        join c in context.Clientes on op.IdCliente equals c.IdCliente
                        select new DtoOrdenProdListado
                        {
                            IdOrdenProduccion = op.IdOrdenProduccion,
                            NumeroOrden = op.NumeroOrden,
                            NombreCliente = c.Nombre,
                            ApellidoCliente = c.Apellido,
                            NombreProd = p.Nombre,
                            NombreUsuario = u.Nombre,
                            EstadoOrden = eop.Descripcion,
                            FechaPedido = op.FechaPedido,
                            FechaEntrega = op.FechaEntrega,
                            Cantidad = op.Cantidad
                        };

            var listaOrdenesProduccion = await query.ToListAsync();

            return listaOrdenesProduccion;
        }
        public async Task<DtoOrdenProd> GetOrdenProduccionById(int id)
        {
            try
            {
                OrdenesProduccione op = await context.OrdenesProducciones.Where(c => c.IdOrdenProduccion.Equals(id)).FirstOrDefaultAsync();
                DtoOrdenProd dto = new DtoOrdenProd();
                dto.IdOrdenProduccion = op.IdOrdenProduccion;
                dto.IdCliente = op.IdCliente;
                dto.IdProducto = op.IdProducto;
                dto.IdUsuario = op.IdUsuario;
                dto.IdEstadoOrdenProduccion = op.IdEstadoOrdenProduccion;
                dto.FechaPedido = op.FechaPedido;
                dto.FechaEntrega = op.FechaEntrega;
                dto.NumeroOrden = op.NumeroOrden;
                dto.Cantidad = op.Cantidad;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<DtoOrdenProd, OrdenesProduccione>();
            }
        }
        public async Task<ResultBase> UpdateStockMateriaPrima(StockMateriasPrima materiaPrima)
        {
            try
            {
                context.StockMateriasPrimas.Update(materiaPrima);
                await context.SaveChangesAsync();

                return new ResultBase
                {
                    Ok = true,
                    CodigoEstado = 200,
                    Message = "El stock de materia prima se actualizó correctamente."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultBase
                {
                    Ok = false,
                    CodigoEstado = 400,
                    Message = "Error al actualizar el stock de materia prima."
                };
            }
        }
        public async Task<Formula> GetFormulaByProductoId(int productoId)
        {
            Formula formula = await context.Formulas
                .SingleOrDefaultAsync(f => f.IdProducto == productoId);

            return formula;
        }

        public async Task<StockMateriasPrima> GetStockMateriaPrimaById(int materiaPrimaId)
        {
            StockMateriasPrima materiaPrima = await context.StockMateriasPrimas
                .SingleOrDefaultAsync(sp => sp.IdMateriaPrima == materiaPrimaId);

            return materiaPrima;
        }

        public async Task<ResultBase> PostOrdenProd(DtoOrdenProd ordenDto)
        {
            ResultBase resultado = new ResultBase();

            try
            {
                // Verificar el stock de materias primas
                bool tieneSuficienteStock = await VerificarStockMateriasPrimas(ordenDto);

                if (tieneSuficienteStock)
                {
                    // Realizar el mapeo del DTO a la entidad
                    var ordenEntity = mapper.Map<OrdenesProduccione>(ordenDto);

                    // Agregar la entidad al contexto de base de datos
                    await context.AddAsync(ordenEntity);
                    await context.SaveChangesAsync();

                    // Obtener la fórmula del producto
                    List<Formula> formulas = await context.Formulas
                        .Where(f => f.IdProducto == ordenDto.IdProducto)
                        .ToListAsync();

                    // Restar la cantidad requerida de materias primas del stock
                    decimal cantidadMateriaPrimaUtilizada = 0; // Variable para almacenar la cantidad de materia prima utilizada

                    foreach (Formula formula in formulas)
                    {
                        StockMateriasPrima stockMateriaPrima = await context.StockMateriasPrimas
                            .FirstOrDefaultAsync(sp => sp.IdMateriaPrima == formula.IdMateriaPrima);

                        decimal cantidadUtilizada = formula.CantidadMateriaPrima;
                        stockMateriaPrima.Cantidad -= cantidadUtilizada;

                        cantidadMateriaPrimaUtilizada += cantidadUtilizada; // Sumar la cantidad utilizada
                    }

                    await context.SaveChangesAsync();

                    // Crear una nueva instancia de Historial_Stock_Materia_Prima para cada materia prima utilizada
                    foreach (Formula formula in formulas)
                    {
                        StockMateriasPrima materiaPrima = await context.StockMateriasPrimas
                            .FirstOrDefaultAsync(sp => sp.IdMateriaPrima == formula.IdMateriaPrima);

                        var historialStockMp = new HistorialStockMateriaPrima
                        {
                            Cantidad = cantidadMateriaPrimaUtilizada, // Utilizar la cantidad total utilizada
                            Precio = materiaPrima.Precio,
                            FechaUltimaActualizacion = DateTime.Now,
                            IdMateriaPrima = formula.IdMateriaPrima,
                            TipoMovimiento = "PRODUCCION"
                        };

                        // Restar la cantidad utilizada de la variable cantidadMateriaPrimaUtilizada
                        cantidadMateriaPrimaUtilizada -= formula.CantidadMateriaPrima;

                        // Guardar la instancia de Historial_Stock_Materia_Prima en la base de datos
                        context.HistorialStockMateriaPrimas.Add(historialStockMp);
                        await context.SaveChangesAsync();

                        // Crear una nueva instancia de Historial_X_StockMP para relacionar los registros
                        var historialXStockMp = new HistorialXStockMp
                        {
                            IdHistorial = historialStockMp.IdHistorial,
                            IdStockMateriaPrima = materiaPrima.IdStockMateriaPrima
                        };

                        // Guardar la instancia de Historial_X_StockMP en la base de datos
                        context.HistorialXStockMps.Add(historialXStockMp);
                        await context.SaveChangesAsync();
                    }

                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "La orden de producción fue cargada con éxito.";
                    return resultado;
                }

                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "No hay suficiente stock de materias primas para fabricar el producto.";
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al registrar la orden de producción.";
                return resultado;
            }
        }

        private async Task<bool> VerificarStockMateriasPrimas(DtoOrdenProd orden)
        {
            // Obtener la fórmula del producto
            List<Formula> formulas = await context.Formulas
                .Where(f => f.IdProducto == orden.IdProducto)
                .ToListAsync();

            // Verificar el stock de cada materia prima en la fórmula
            foreach (Formula formula in formulas)
            {
                StockMateriasPrima stockMateriaPrima = await context.StockMateriasPrimas
                    .FirstOrDefaultAsync(sp => sp.IdMateriaPrima == formula.IdMateriaPrima);

                if (stockMateriaPrima == null || stockMateriaPrima.Cantidad < formula.CantidadMateriaPrima)
                {
                    return false; // No hay suficiente stock
                }
            }

            return true; // Tiene suficiente stock de todas las materias primas
        }

        public async Task<ResultBase> PutEstado(DtoEstadoOrden estado)
        {
            ResultBase resultado = new ResultBase();
            var orden = await context.OrdenesProducciones
                .Include(o => o.IdProductoNavigation) // Incluye la entidad relacionada
                .FirstOrDefaultAsync(c => c.NumeroOrden.Equals(estado.NumeroOrden));

            try
            {
                if (orden != null)
                {
                    int estadoAnterior = orden.IdEstadoOrdenProduccion;
                    orden.IdEstadoOrdenProduccion = estado.IdEstadoOrdenProduccion;

                    context.Update(orden);
                    await context.SaveChangesAsync();

                    // Realizar acciones adicionales según el estado
                    if (estadoAnterior != estado.IdEstadoOrdenProduccion)
                    {
                        switch (estado.IdEstadoOrdenProduccion)
                        {
                            case 2: // Cancelada
                                await DevolverMateriasPrimas(orden);
                                break;
                            case 3: // Finalizada
                                await AgregarProductos(orden);
                                break;
                        }
                    }

                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "La orden se modificó exitosamente.";
                }
                else
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Error al modificar la orden";
                }
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Error = ex.ToString();
                resultado.Message = "Error al modificar la orden";
            }

            return resultado;
        }

        private async Task DevolverMateriasPrimas(OrdenesProduccione orden)
        {
            var formulas = await context.Formulas
                .Where(f => f.IdProducto == orden.IdProducto)
                .ToListAsync();

            foreach (var formula in formulas)
            {
                var materiaPrima = await context.StockMateriasPrimas
                    .FirstOrDefaultAsync(mp => mp.IdMateriaPrima == formula.IdMateriaPrima);

                if (materiaPrima != null)
                {
                    decimal cantidadDevuelta = (decimal)(formula.CantidadMateriaPrima * orden.Cantidad);
                    materiaPrima.Cantidad += cantidadDevuelta;
                    context.Update(materiaPrima);
                }
            }

            await context.SaveChangesAsync();
        }

        private async Task AgregarProductos(OrdenesProduccione orden) 
        {
            var stockProducto = await context.StockProductos
                .FirstOrDefaultAsync(sp => sp.IdProducto == orden.IdProducto);

            if (stockProducto != null)
            {
                stockProducto.Cantidad += (int)orden.Cantidad;
                context.Update(stockProducto);
                await context.SaveChangesAsync();

                // Crear una nueva instancia de HistorialStockProducto
                var historialStockProd = new HistorialStockProducto
                {

                    IdProducto = stockProducto.IdProducto,
                    Cantidad = stockProducto.Cantidad,
                    FechaUltimaActualizacion = DateTime.Now,
                    TipoMovimiento = "PRODUCCIÓN"
                };

                // Guardar la instancia de HistorialStockProducto en la base de datos
                context.HistorialStockProductos.Add(historialStockProd);
                await context.SaveChangesAsync();


                // Crear una nueva instancia de Historial_X_StockP para relacionar los registros
                var historialXStockP = new HistorialXStockP
                {
                    IdHistorialProd = historialStockProd.IdHistorialProd,
                    IdStockProducto = stockProducto.IdStockProducto
                };

                // Guardar la instancia de Historial_X_StockP en la base de datos
                context.HistorialXStockPs.Add(historialXStockP);
                await context.SaveChangesAsync();

            }
        }

        public async Task<ResultBase> PutOrdenProd(DtoOrdenProd ordenDto)
        {
            ResultBase resultado = new ResultBase();

            try
            {
                // Obtener la fórmula del producto
                Formula formula = await GetFormulaByProductoId(ordenDto.IdProducto);

                // Obtener la orden de producción existente por su ID
                OrdenesProduccione ordenExistente = await context.OrdenesProducciones
                    .FirstOrDefaultAsync(o => o.IdOrdenProduccion == ordenDto.IdOrdenProduccion);

                if (ordenExistente == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = "La orden de producción no existe.";
                    return resultado;
                }

                // Verificar si hay suficiente stock de materia prima
                decimal cantidadMateriaPrimaNecesaria = formula.CantidadMateriaPrima * ((ordenDto.Cantidad ?? 0) - (ordenExistente.Cantidad ?? 0));
                StockMateriasPrima materiaPrima = await GetStockMateriaPrimaById(formula.IdMateriaPrima);

                if (materiaPrima == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "No se encontró la materia prima en el stock.";
                    return resultado;
                }

                if (cantidadMateriaPrimaNecesaria > materiaPrima.Cantidad)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "No hay suficiente stock de materias primas para fabricar el producto.";
                    return resultado;
                }

                // Guardar el stock de materia prima antes de la actualización
                decimal stockAnterior = materiaPrima.Cantidad;

                // Restar la cantidad necesaria de materias primas del stock
                materiaPrima.Cantidad -= cantidadMateriaPrimaNecesaria;
                await UpdateStockMateriaPrima(materiaPrima);

                // Crear una nueva instancia de Historial_Stock_Materia_Prima
                var historialStockMp = new HistorialStockMateriaPrima
                {
                    Cantidad = cantidadMateriaPrimaNecesaria,
                    Precio = materiaPrima.Precio,
                    FechaUltimaActualizacion = DateTime.Now,
                    IdMateriaPrima = formula.IdMateriaPrima,
                    TipoMovimiento = cantidadMateriaPrimaNecesaria > 0 ? "PRODUCCION" : "DEVOLUCION"
                };

                // Guardar la instancia de Historial_Stock_Materia_Prima en la base de datos
                context.HistorialStockMateriaPrimas.Add(historialStockMp);
                await context.SaveChangesAsync();

                // Crear una nueva instancia de Historial_X_StockMP para relacionar los registros
                var historialXStockMp = new HistorialXStockMp
                {
                    IdHistorial = historialStockMp.IdHistorial,
                    IdStockMateriaPrima = materiaPrima.IdStockMateriaPrima
                };

                // Guardar la instancia de Historial_X_StockMP en la base de datos
                context.HistorialXStockMps.Add(historialXStockMp);
                await context.SaveChangesAsync();

                // Actualizar los datos de la orden existente

                ordenExistente.IdCliente = ordenDto.IdCliente;
                ordenExistente.IdProducto = ordenDto.IdProducto;
                ordenExistente.IdUsuario = ordenDto.IdUsuario;
                ordenExistente.IdEstadoOrdenProduccion = ordenDto.IdEstadoOrdenProduccion;
                ordenExistente.FechaPedido = ordenDto.FechaPedido;
                ordenExistente.FechaEntrega = ordenDto.FechaEntrega;
                ordenExistente.NumeroOrden = ordenDto.NumeroOrden;
                ordenExistente.Cantidad = ordenDto.Cantidad;



                // Guardar los cambios en la base de datos
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "La orden de producción se actualizó con éxito.";
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al actualizar la orden de producción.";
                return resultado;
            }
        }
    }
}
