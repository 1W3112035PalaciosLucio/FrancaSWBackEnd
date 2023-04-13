using FrancaSW.Models;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceDisenioProducto
    {
        Task<List<DiseniosProducto>> GetDisenio();
    }
}
