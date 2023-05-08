using FrancaSW.DTO;
using FrancaSW.Models;

namespace FrancaSW.Services
{
    public interface IServiceHistorialStockMP
    {
        Task<List<HistorialStockMateriaPrima>> GetListadoHistorialStockMP();
        Task<List<DtoListadoHistorialStockMateriaPrima>> GetListaHistStockMPById(int id);
    }
}
