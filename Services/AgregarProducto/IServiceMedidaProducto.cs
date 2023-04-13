using FrancaSW.Models;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceMedidaProducto
    {
        Task<List<MedidasProducto>> GetMedida();
    }
}
