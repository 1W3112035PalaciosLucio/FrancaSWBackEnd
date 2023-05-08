using FrancaSW.DTO;
using FrancaSW.Models;

namespace FrancaSW.Services
{
    public interface IServiceHistorialStockProductos
    {
        Task<List<HistorialStockProducto>> GetListadoHistorialStockProductos();
        Task<List<DtoListadoHistorialStockProductos>> GetListaHistStockProductosById(int id);
    }
}
