using FrancaSW.Commands;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceStockProductos
    {
        Task<List<DtoListadoStockProductos>> GetListadoStockProducto();
        Task<DtoStockProducto> GetStockProductoById(int id);
        Task<ResultBase> PostStockProducto(StockProducto stockP);
        Task<ResultBase> PutStockProducto(CommandStockProductos stockP);
    }
}
