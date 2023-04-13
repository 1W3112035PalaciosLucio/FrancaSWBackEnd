using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceMateriaPrima
    {
        Task<List<MateriasPrima>> GetMateriaPrima();
        Task<ResultBase> PostMateriaPrima(MateriasPrima mp);
        Task<ResultBase> PutMateriaPrima(DTOMateriaPrima dtoMp);
        Task<DTOMateriaPrima> GetMateriaPrimaById(int id);
        
    }
}
