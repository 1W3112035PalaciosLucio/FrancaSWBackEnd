using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceOrdenesProduccion
    {
        Task<List<DtoOrdenProdListado>> GetListadoOrdenProduccion();
        Task<DtoOrdenProd> GetOrdenProduccionById(int id);
        Task<ResultBase> PostOrdenProd(DtoOrdenProd orden);
        Task<ResultBase> PutOrdenProd(DtoOrdenProd orden);
        Task<ResultBase> PutEstado(DtoEstadoOrden estado);

        Task<Formula> GetFormulaByProductoId(int productoId);
        Task<StockMateriasPrima> GetStockMateriaPrimaById(int materiaPrimaId);
        Task<ResultBase> UpdateStockMateriaPrima(StockMateriasPrima materiaPrima);


    }
}
