using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.DTO.Reportes;
using FrancaSW.Models;
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

        public async Task<List<DtoListaReportePrecioMP>> GetListadoReportePrecioMP()
        {
            var query = (from hsmp in context.HistorialStockMateriaPrimas
                         join smp in context.StockMateriasPrimas on hsmp.IdMateriaPrima equals smp.IdMateriaPrima
                         join mp in context.MateriasPrimas on hsmp.IdMateriaPrima equals mp.IdMateriaPrima
                         select new DtoListaReportePrecioMP
                         {
                             Descripcion = mp.Descripcion,
                             Precio = hsmp.Precio,
                             FechaUltimaActualizacion = hsmp.FechaUltimaActualizacion.ToString("dd-MM-yyyy")
                         }).ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporteOrdenPendiente>> GetListadoReporteOrdenPendiente()
        {
            var query = (from op in context.OrdenesProducciones
                         join eop in context.EstadosOrdenesProducciones on op.IdEstadoOrdenProduccion equals eop.IdEstadoOrdenProduccion
                         join p in context.Productos on op.IdProducto equals p.IdProducto
                         where op.IdEstadoOrdenProduccion == 1
                         group op by p.Nombre into g
                         select new DtoListaReporteOrdenPendiente
                         {
                             Nombre = g.Key,
                             Cantidad = g.Sum(op => op.Cantidad)
                         }).ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporteOrdenPendienteMp>> GetListadoReporteOrdenPendienteMp()
        {
            var query = (from op in context.OrdenesProducciones
                         join eop in context.EstadosOrdenesProducciones on op.IdEstadoOrdenProduccion equals eop.IdEstadoOrdenProduccion
                         join p in context.Productos on op.IdProducto equals p.IdProducto
                         join f in context.Formulas on p.IdProducto equals f.IdProducto
                         join mp in context.MateriasPrimas on f.IdMateriaPrima equals mp.IdMateriaPrima
                         where op.IdEstadoOrdenProduccion == 1
                         group f by mp.Descripcion into g
                         select new DtoListaReporteOrdenPendienteMp
                         {
                             Descripcion = g.Key,
                             CantidadMateriaPrima = g.Sum(f => f.CantidadMateriaPrima)
                         }).ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporteMPDisponible>> GetListadoReporteMPDisponible()
        {
            var query = (from op in context.OrdenesProducciones
                         join eop in context.EstadosOrdenesProducciones on op.IdEstadoOrdenProduccion equals eop.IdEstadoOrdenProduccion
                         join p in context.Productos on op.IdProducto equals p.IdProducto
                         join f in context.Formulas on p.IdProducto equals f.IdProducto
                         join mp in context.MateriasPrimas on f.IdMateriaPrima equals mp.IdMateriaPrima
                         join smp in context.StockMateriasPrimas on mp.IdMateriaPrima equals smp.IdMateriaPrima
                         where op.IdEstadoOrdenProduccion == 1
                         group new { mp, smp } by new { mp.Descripcion, smp.Cantidad } into g
                         select new DtoListaReporteMPDisponible
                         {
                             Descripcion = g.Key.Descripcion,
                             Cantidad = g.Key.Cantidad
                         }).ToListAsync();

            return await query;
        }

        public async Task<List<DtoListaReporteMPStockMinimo>> GetListadoReporteMPStockMinimo()
        {
            var query = (from smp in context.StockMateriasPrimas
                         join mp in context.MateriasPrimas on smp.IdMateriaPrima equals mp.IdMateriaPrima
                         where smp.Cantidad < smp.StockMinimo
                         orderby smp.Cantidad ascending
                         select new DtoListaReporteMPStockMinimo
                         {
                             Descripcion = mp.Descripcion,
                             Cantidad = smp.Cantidad
                         }).ToListAsync();

            return await query;
        }
    }
}
