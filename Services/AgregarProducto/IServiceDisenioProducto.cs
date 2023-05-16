using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceDisenioProducto
    {
        Task<List<DiseniosProducto>> GetDisenio();
        Task<ResultBase> PostDisenio(DiseniosProducto disenio);
    }
}
