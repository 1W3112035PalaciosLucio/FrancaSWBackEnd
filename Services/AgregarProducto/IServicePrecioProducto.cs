using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;


namespace FrancaSW.Services.AgregarProducto
{
    public interface IServicePrecioProducto
    {
        Task<List<DtoPrecioBocha>> GetPrecio();
        Task<List<PreciosBocha>> GetPrecios();
        Task<ResultBase> PostPrecio(PreciosBocha precios);
    }
}
