using FrancaSW.DTO.Reportes;

namespace FrancaSW.Services
{
    public interface IServiceReportes
    {
        Task<List<DtoListadoReportes>> GetListadoReporteStockProd();
        Task<List<DtoListaReporte>> GetListadoReporteStockProd1();
        Task<List<DtoListaReporteMP>> GetListadoReporteStockMP();

    }
}
