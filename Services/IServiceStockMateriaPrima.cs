using FrancaSW.Commands;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceStockMateriaPrima
    {
        Task<List<DtoListadoStockMateriaPrima>> GetListadoStockMateriaPrima();
        Task<DtoStockMP> GetStockMateriaPrimaById(int id);
        Task<ResultBase> PostStockMP(StockMateriasPrima stockMp);
        Task<ResultBase> PutStockMP(CommandStockMateriaPrima stockMp);
    }
}
