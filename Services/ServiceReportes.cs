using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.DTO.Reportes;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceReportes : IServiceReportes
    {
        public readonly FrancaSwContext context;
        public readonly ISecurityService securityService;

        public ServiceReportes(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        public async Task<List<DtoListadoReportes>> GetListadoReporteStockProd()
        {
            var query = (from p in context.Productos
                         join sp in context.StockProductos on p.IdProducto equals sp.IdProducto
                         join mp in context.MedidasProductos on p.IdMedidaProducto equals mp.IdMedidaProducto
                         join pb in context.PreciosBochas on p.IdPrecioBocha equals pb.IdPreciosBocha
                         join tp in context.TiposProductos on p.IdTipoProducto equals tp.IdTipoProducto
                         join dp in context.DiseniosProductos on p.IdDisenioProducto equals dp.IdDisenio
                         join hsp in context.HistorialStockProductos on p.IdProducto equals hsp.IdProducto
                         select new DtoListadoReportes
                         {
                             idProducto = p.IdProducto,
                             Codigo = p.Codigo,
                             Nombre = p.Nombre,
                             ColorProducto = p.IdColorProductoNavigation.Descripcion,
                             DisenioProducto = p.IdDisenioProductoNavigation.Descripcion,
                             TipoProducto = p.IdTipoProductoNavigation.Descripcion,
                             MedidaProducto = p.IdMedidaProductoNavigation.Descripcion,
                             PrecioBocha = p.IdPrecioBochaNavigation.Precio,
                             FechaUltimaActualizacion = sp.FechaUltimaActualizacion,
                             Cantidad = sp.Cantidad
                         }).Distinct().ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporte>> GetListadoReporteStockProd1()
        {
            var query = (from p in context.Productos
                         join sp in context.StockProductos on p.IdProducto equals sp.IdProducto
                         join mp in context.MedidasProductos on p.IdMedidaProducto equals mp.IdMedidaProducto
                         join pb in context.PreciosBochas on p.IdPrecioBocha equals pb.IdPreciosBocha
                         join tp in context.TiposProductos on p.IdTipoProducto equals tp.IdTipoProducto
                         join dp in context.DiseniosProductos on p.IdDisenioProducto equals dp.IdDisenio
                         join hsp in context.HistorialStockProductos on p.IdProducto equals hsp.IdProducto
                         select new DtoListaReporte
                         {
                             Nombre = p.Nombre,
                             Cantidad = sp.Cantidad
                         }).Distinct().ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporteMP>> GetListadoReporteStockMP()
        {
            var query = (from mp in context.MateriasPrimas
                         join smp in context.StockMateriasPrimas on mp.IdMateriaPrima equals smp.IdMateriaPrima
                         join hsmp in context.HistorialStockMateriaPrimas on mp.IdMateriaPrima equals hsmp.IdMateriaPrima
                         select new DtoListaReporteMP
                         {
                             Descripcion = mp.Descripcion,
                             Cantidad = smp.Cantidad
                           
                         }).Distinct().ToListAsync();

            return await query;
        }
    }
}
