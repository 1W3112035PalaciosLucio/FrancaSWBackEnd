using FrancaSW.Models;

namespace FrancaSW.Services
{
    public interface IServiceProvinciaLocalidad
    {
        Task<IEnumerable<Localidade>> GetLocalidadesByProvincia(int idProvincia);
        Task<List<Provincia>> GetProvincia();
        Task<List<Localidade>> GetLocalidades();
    }
}
