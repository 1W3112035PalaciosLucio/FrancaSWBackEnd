using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services
{
    public interface IServiceFormula
    {
        Task<List<DtoListadoFormula>> GetListadoFormula();
        Task<DtoFormula> GetFormulaById(int id);
        Task<ResultBase> PostFormula(Formula formula);
        Task<ResultBase> PutFormula(DtoFormula dtoFormula);
    }
}
