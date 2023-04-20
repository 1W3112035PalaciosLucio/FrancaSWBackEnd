using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceProveedor
    {
        Task<List<Proveedore>> GetProveedor();
        Task<ResultBase> PostProveedor(Proveedore proveedor);
        Task<ResultBase> PutProveedor(DtoProveedor dtoProveedor);
        Task<DtoProveedor> GetProveedorById(int id);
        Task<DtoProveedorId> GetProveedorByIdd(int id);
        Task<List<DtoListadoProveedor>> GetListadoProveedor();
        Task<ResultBase> DesactivarProveedor(int id);
        Task<ResultBase> ActivarProveedor(int id);
    }
}
