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
        Task<List<EstadosOrdenesProduccione>> GetEstado1();
        Task<List<EstadosOrdenesProduccione>> GetEstado2();
    }
}
