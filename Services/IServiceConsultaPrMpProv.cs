using FrancaSW.DTO;

namespace FrancaSW.Services
{
    public interface IServiceConsultaPrMpProv
    {
        Task<List<DtoConsultaPrMPbyProveedor>> ObtenerPreciosPorProveedor(int idProveedor);
        Task<List<DtoConsultaPrMPbyProveedor>> ObtenerPreciosPorMateriaPrima(int idMateriaPrima);
    }
}
