using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceHistorialStockMP : IServiceHistorialStockMP
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceHistorialStockMP(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        public async Task<List<HistorialStockMateriaPrima>> GetListadoHistorialStockMP()
        {
            return await context.HistorialStockMateriaPrimas.AsNoTracking().ToListAsync();
        }
        public async Task<List<DtoListadoHistorialStockMateriaPrima>> GetListaHistStockMPById(int id)
        {
            //try
            //{
            //    return await context.HistorialXStockMps
            //    .Where(c => c.IdStockMateriaPrimaNavigation.IdMateriaPrima.Equals(id))
            //    .OrderByDescending(stock => stock.IdHistorial)
            //    .Select(stock => new DtoListadoHistorialStockMateriaPrima
            //    {
            //        IdHistorial = stock.IdHistorial,
            //        IdMateriaPrima = stock.IdStockMateriaPrimaNavigation.IdMateriaPrima,
            //        Descripcion = stock.IdStockMateriaPrimaNavigation.IdMateriaPrimaNavigation.Descripcion,
            //        Cantidad = stock.IdStockMateriaPrimaNavigation.Cantidad,
            //        Precio = stock.IdStockMateriaPrimaNavigation.Precio,
            //        FechaUltimaActualizacion = stock.IdStockMateriaPrimaNavigation.FechaUltimaActualizacion,
            //        TipoMovimiento = stock.IdHistorialNavigation.TipoMovimiento
            //    })
            //    .ToListAsync();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            try
            {
                return await context.HistorialXStockMps
                    .Where(c => c.IdStockMateriaPrimaNavigation.IdMateriaPrima.Equals(id))
                    .OrderByDescending(stock => stock.IdHistorial)
                    .Select(stock => new DtoListadoHistorialStockMateriaPrima
                    {
                        IdHistorial = stock.IdHistorial,
                        IdMateriaPrima = stock.IdStockMateriaPrimaNavigation.IdMateriaPrima,
                        Descripcion = stock.IdStockMateriaPrimaNavigation.IdMateriaPrimaNavigation.Descripcion,
                        Cantidad = stock.IdHistorialNavigation.Cantidad, // Utilizar la cantidad del historial en lugar de la cantidad del stock
                        Precio = stock.IdStockMateriaPrimaNavigation.Precio,
                        FechaUltimaActualizacion = stock.IdStockMateriaPrimaNavigation.FechaUltimaActualizacion,
                        TipoMovimiento = stock.IdHistorialNavigation.TipoMovimiento
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
