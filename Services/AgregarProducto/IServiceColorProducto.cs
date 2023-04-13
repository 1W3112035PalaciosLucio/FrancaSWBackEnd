using FrancaSW.Commands.Combos;
using FrancaSW.Models;

namespace FrancaSW.Services.AgregarProducto
{
    public interface IServiceColorProducto
    {
        Task<List<ColoresProducto>> GetColor();
        Task<List<CombosItems>> GetColorForComboBox();
    }
}
