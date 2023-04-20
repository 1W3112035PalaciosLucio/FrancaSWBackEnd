using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServicePrecioMatPrimaProv
    {
        Task<ResultBase> PostPrecioMatPrimaProv(PreciosMateriasPrimasProveedore mp);
        Task<List<DtoListaPrMatPrProv>> GetListaPrecioMatPrimaProv();
        Task<ResultBase> PutPrecioMatPrimaProv(DtoPrecioMatPrimaProv dtoPrecio);
        Task<DtoListaPrMatPrProv> GetPrecioMatPrimaProvById(int id);
        Task<DtoListaPrecioMatPrProv> GetPrecioMatPrimaProvByIdd(int id);
        Task<List<DtoListaPrecioMatPrProv>> GetAllPrecioMatPrimaProv();
    }
}
