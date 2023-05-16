using FrancaSW.Commands.Combos;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceColorProducto
    {
        Task<List<ColoresProducto>> GetColor();
        Task<List<CombosItems>> GetColorForComboBox();
        Task<ResultBase> PostColor(ColoresProducto color);
    }
}
