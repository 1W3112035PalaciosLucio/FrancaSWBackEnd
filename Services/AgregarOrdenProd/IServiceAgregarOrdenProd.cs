using FrancaSW.Models;

namespace FrancaSW.Services.AgregarOrdenProd
{
    public interface IServiceAgregarOrdenProd
    {
        Task<List<Cliente>> GetNCliente();
        Task<List<Cliente>> GetACliente();
        Task<List<Producto>> GetProducto();
        Task<List<Usuario>> GetUsuario();
        Task<List<EstadosOrdenesProduccione>> GetEstado();
    }
}
