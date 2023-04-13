using FrancaSW.Models;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceTipoProducto
    {
        Task<List<TiposProducto>> GetTipoProducto();
    }
}
