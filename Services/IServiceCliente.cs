using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceCliente
    {
        Task<List<Cliente>> GetCliente();
        Task<ResultBase> PostCliente(Cliente cliente);
        Task<ResultBase> PutCliente(DtoCliente dtoCliente);
        Task<DtoClienteId> GetClienteById(int id);
        Task<List<DtoListadoCliente>> GetListadoCliente();
    }
}
