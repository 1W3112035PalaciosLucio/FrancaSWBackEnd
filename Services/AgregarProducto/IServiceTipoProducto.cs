using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceTipoProducto
    {
        Task<List<TiposProducto>> GetTipoProducto();
        Task<ResultBase> PostTipoProd(TiposProducto tipo);
    }
}
