using FrancaSW.DTO.Reportes;

namespace FrancaSW.Services
{
    public interface IServiceReportes
    {
        Task<List<DtoListadoReportes>> GetListadoReporteStockProd();
        Task<List<DtoListaReporte>> GetListadoReporteStockProd1();
        Task<List<DtoListaReporteMP>> GetListadoReporteStockMP();
        Task<List<DtoListaReportePrecioMP>> GetListadoReportePrecioMP();
        Task<List<DtoListaReporteOrdenPendiente>> GetListadoReporteOrdenPendiente();
        Task<List<DtoListaReporteOrdenPendienteMp>> GetListadoReporteOrdenPendienteMp();
        Task<List<DtoListaReporteMPDisponible>> GetListadoReporteMPDisponible();
        Task<List<DtoListaReporteMPStockMinimo>> GetListadoReporteMPStockMinimo();
    }
}
