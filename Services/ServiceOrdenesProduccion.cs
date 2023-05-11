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

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<DtoOrdenProd, OrdenesProduccione>();
            }
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
                    foreach (Formula formula in formulas)
                    {
                        StockMateriasPrima stockMateriaPrima = await context.StockMateriasPrimas
                            .FirstOrDefaultAsync(sp => sp.IdMateriaPrima == formula.IdMateriaPrima);

                        stockMateriaPrima.Cantidad -= formula.CantidadMateriaPrima;
                    }

                    await context.SaveChangesAsync();

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


        //public async Task<ResultBase> PutEstado(DtoEstadoOrden estado)
        //{
        //    ResultBase resultado = new ResultBase();
        //    var orden = await context.OrdenesProducciones.Where(c => c.NumeroOrden.Equals(estado.NumeroOrden)).FirstOrDefaultAsync();
        //    try
        //    {
        //        if (orden != null)
        //        {
        //            orden.IdEstadoOrdenProduccion = estado.IdEstadoOrdenProduccion;

        //            context.Update(orden);

        //            await context.SaveChangesAsync();
        //            resultado.Ok = true;
        //            resultado.CodigoEstado = 200;
        //            resultado.Message = "La orden se modifico exitosamente.";
        //        }

        //        else
        //        {
        //            resultado.Ok = false;
        //            resultado.CodigoEstado = 400;
        //            resultado.Message = "Error al modificar la orden";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Ok = false;
        //        resultado.CodigoEstado = 400;
        //        resultado.Error = ex.ToString();
        //        resultado.Message = "Error al modificar la orden";
        //    }

        //    return resultado;
        //}


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
            }
        }
    }
}
