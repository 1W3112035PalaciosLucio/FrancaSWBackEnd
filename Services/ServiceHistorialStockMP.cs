using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.DTO.Reportes;
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
            try
            {
                var query = (from mp in context.MateriasPrimas
                             join smp in context.StockMateriasPrimas on mp.IdMateriaPrima equals smp.IdMateriaPrima
                             join hsmp in context.HistorialStockMateriaPrimas on smp.IdMateriaPrima equals hsmp.IdMateriaPrima
                             where mp.IdMateriaPrima == id
                             orderby hsmp.IdHistorial descending
                             select new DtoListadoHistorialStockMateriaPrima
                             {
                                 IdHistorial = hsmp.IdHistorial,
                                 IdMateriaPrima = smp.IdMateriaPrima,
                                 Descripcion = mp.Descripcion,
                                 Cantidad = hsmp.Cantidad,
                                 Precio = hsmp.Precio,
                                 FechaUltimaActualizacion = hsmp.FechaUltimaActualizacion,
                                 TipoMovimiento = hsmp.TipoMovimiento


                             }).ToListAsync();
                return await query;

                //return await context.HistorialXStockMps
                //    .Where(c => c.IdStockMateriaPrimaNavigation.IdMateriaPrima.Equals(id))
                //    .OrderByDescending(stock => stock.IdHistorial)
                //    .Select(stock => new DtoListadoHistorialStockMateriaPrima
                //    {
                //        IdHistorial = stock.IdHistorial,
                //        IdMateriaPrima = stock.IdStockMateriaPrimaNavigation.IdMateriaPrima,
                //        Descripcion = stock.IdStockMateriaPrimaNavigation.IdMateriaPrimaNavigation.Descripcion,
                //        Cantidad = stock.IdHistorialNavigation.Cantidad, // Utilizar la cantidad del historial en lugar de la cantidad del stock
                //        Precio = stock.IdStockMateriaPrimaNavigation.Precio,
                //        FechaUltimaActualizacion = stock.IdStockMateriaPrimaNavigation.FechaUltimaActualizacion,
                //        TipoMovimiento = stock.IdHistorialNavigation.TipoMovimiento
                //    })
                //    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
