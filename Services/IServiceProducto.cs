using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceProducto
    {
        Task<List<Producto>> GetProducto();
        Task<ResultBase> PostProducto(Producto producto);
        Task<ResultBase> PutProducto(DTOProducto dtoProducto);
        Task<DTOProducto> GetProductoById(int id);
        Task<List<DtoListadoProductos>> GetListadoProductos();
        Task<ResultBase> DesactivarProducto(int id);
        Task<ResultBase> ActivarProducto(int id);

        Task<DTOProducto> GetProdByCodigo(int codigo);
    }
}
