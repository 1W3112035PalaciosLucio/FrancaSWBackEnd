using FrancaSW.DTO;


namespace FrancaSW.Services.AgregarProducto
{
    public interface IServicePrecioProducto
    {
        Task<List<DtoPrecioBocha>> GetPrecio();
    }
}
