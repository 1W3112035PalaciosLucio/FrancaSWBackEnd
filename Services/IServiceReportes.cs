using FrancaSW.DTO.Reportes;

namespace FrancaSW.Services
{
    public interface IServiceReportes
    {
        Task<List<DtoListadoReportes>> GetListadoReporteStockProd();
    }
}
