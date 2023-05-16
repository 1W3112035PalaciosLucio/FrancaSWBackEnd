using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceMedidaProducto
    {
        Task<List<MedidasProducto>> GetMedida();
        Task<ResultBase> PostMedida(MedidasProducto medida);
    }
}
