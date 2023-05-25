using FrancaSW.Commands;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceStockProductos : IServiceStockProductos
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceStockProductos(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        public async Task<List<DtoListadoStockProductos>> GetListadoStockProducto()
        {
            return await context.StockProductos.AsNoTracking().
                Select(x => new DtoListadoStockProductos
                {
                    IdStockProducto = x.IdStockProducto,
                    IdProducto = x.IdProducto,
                    Codigo = x.IdProductoNavigation.Codigo,
                    Nombre = x.IdProductoNavigation.Nombre,
                    Cantidad = x.Cantidad,
                    Precio = x.IdProductoNavigation.IdPrecioBochaNavigation.Precio,
                    FechaUltimaActualizacion = x.FechaUltimaActualizacion

                }).OrderBy(x=>x.Codigo).ToListAsync();
        }

        public async Task<DtoStockProducto> GetStockProductoById(int id)
        {
            try
            {
                StockProducto stock = await context.StockProductos
                    .Where(c => c.IdStockProducto.Equals(id)).FirstOrDefaultAsync();
                DtoStockProducto dto = new DtoStockProducto();
                dto.IdStockProducto = stock.IdStockProducto;
                dto.IdProducto = stock.IdProducto;
                dto.Cantidad = stock.Cantidad;
                dto.FechaUltimaActualizacion = stock.FechaUltimaActualizacion;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ResultBase> PostStockProducto(StockProducto stockP)
        {
            ResultBase resultado = new ResultBase();

            var stockPExist = await context.StockProductos.FindAsync(stockP.IdProducto);
            if (stockPExist != null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Ya hay registros de este producto en el stock";
                return resultado;
            }

            // Verificar si el producto existe en la base de datos
            if (await context.Productos.FindAsync(stockP.IdProducto) == null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "El producto no existe en la base de datos";
                return resultado;
            }
            else
            {

                try
                {
                    await context.AddAsync(stockP);
                    await context.SaveChangesAsync();



                    // Crear una nueva instancia de HistorialStockProducto
                    var historialStockProd = new HistorialStockProducto
                    {

                        IdProducto = stockP.IdProducto,
                        Cantidad = stockP.Cantidad,
                        FechaUltimaActualizacion = DateTime.Now,
                        TipoMovimiento = "NUEVO"
                    };

                    // Guardar la instancia de HistorialStockProducto en la base de datos
                    context.HistorialStockProductos.Add(historialStockProd);
                    await context.SaveChangesAsync();


                    // Crear una nueva instancia de Historial_X_StockP para relacionar los registros
                    var historialXStockP = new HistorialXStockP
                    {
                        IdHistorialProd = historialStockProd.IdHistorialProd,
                        IdStockProducto = stockP.IdStockProducto
                    };

                    // Guardar la instancia de Historial_X_StockP en la base de datos
                    context.HistorialXStockPs.Add(historialXStockP);
                    await context.SaveChangesAsync();

                    resultado.Ok = true;
                    resultado.CodigoEstado = 200;
                    resultado.Message = "El stock fue registrado con éxito";
                    return resultado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Error al registrar el stock";
                    return resultado;
                }
            }
        }

        public async Task<ResultBase> PutStockProducto(CommandStockProductos stockP)
        {
            ResultBase resultado = new ResultBase();

            var stockPExist = await context.StockProductos.FirstOrDefaultAsync(c => c.IdStockProducto == stockP.IdStockProducto);
            if (stockPExist == null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "El stock no existe en la base de datos";
                return resultado;
            }


            try
            {
                stockPExist.Cantidad = stockP.Cantidad;
                stockPExist.FechaUltimaActualizacion = stockP.FechaUltimaActualizacion;

                await context.SaveChangesAsync();
                           

                // Crear una nueva instancia de HistorialStockProducto
                var historialStockProd = new HistorialStockProducto
                {

                    IdProducto = stockP.IdProducto,
                    Cantidad = stockP.Cantidad,
                    FechaUltimaActualizacion = DateTime.Now,
                    TipoMovimiento = stockP.TipoMovimiento
                };

                // Guardar la instancia de HistorialStockProducto en la base de datos
                context.HistorialStockProductos.Add(historialStockProd);
                await context.SaveChangesAsync();


                // Crear una nueva instancia de Historial_X_StockP para relacionar los registros
                var historialXStockP = new HistorialXStockP
                {
                    IdHistorialProd = historialStockProd.IdHistorialProd,
                    IdStockProducto = stockP.IdStockProducto
                };

                // Guardar la instancia de Historial_X_StockP en la base de datos
                context.HistorialXStockPs.Add(historialXStockP);
                await context.SaveChangesAsync();





                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El stock fue actualizado con éxito";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al actualizar el stock";
                return resultado;
            }
        }

    }
}
