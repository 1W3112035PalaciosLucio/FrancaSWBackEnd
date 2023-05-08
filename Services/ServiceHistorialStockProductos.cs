using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceHistorialStockProductos : IServiceHistorialStockProductos
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceHistorialStockProductos(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        public async Task<List<HistorialStockProducto>> GetListadoHistorialStockProductos()
        {
            return await context.HistorialStockProductos.AsNoTracking().ToListAsync();
        }

        public async Task<List<DtoListadoHistorialStockProductos>> GetListaHistStockProductosById(int id)
        {
            //try
            //{
            //    return await context.HistorialStockProductos
            //    .Where(c => c.IdProducto.Equals(id))
            //    .OrderByDescending(stock => stock.IdHistorialProd)
            //    .Select(stock => new DtoListadoHistorialStockProductos
            //    {
            //        IdHistorialProd = stock.IdHistorialProd,
            //        IdProducto = stock.IdProducto,
            //        Nombre = stock.IdProductoNavigation.Nombre,
            //        Cantidad = stock.Cantidad,
            //        FechaUltimaActualizacion = stock.FechaUltimaActualizacion,
            //        TipoMovimiento = stock.TipoMovimiento
            //    })
            //    .ToListAsync();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            try
            {
                return await context.HistorialXStockPs
                .Where(c => c.IdHistorialProdNavigation.IdProducto.Equals(id))
                .OrderByDescending(stock => stock.IdHistorialProd)
                .Select(stock => new DtoListadoHistorialStockProductos
                {
                    IdHistorialProd = stock.IdHistorialProd,
                    IdProducto = stock.IdHistorialProdNavigation.IdProducto,
                    Nombre = stock.IdStockProductoNavigation.IdProductoNavigation.Nombre,
                    Cantidad = stock.IdStockProductoNavigation.Cantidad,
                    FechaUltimaActualizacion = stock.IdStockProductoNavigation.FechaUltimaActualizacion,
                    TipoMovimiento = stock.IdHistorialProdNavigation.TipoMovimiento
                })
                .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
