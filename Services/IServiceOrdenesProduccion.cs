using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceOrdenesProduccion
    {
        Task<List<DtoOrdenProdListado>> GetListadoOrdenProduccion();
        Task<ResultBase> PostOrdenProd(DtoOrdenProd orden);
    
    }
}
