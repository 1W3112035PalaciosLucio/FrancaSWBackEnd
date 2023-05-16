using FrancaSW.Commands.Combos;
using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Results;
using Microsoft.EntityFrameworkCore;


namespace FrancaSW.Services.AgregarProducto
{
    public class ServiceColorProducto : IServiceColorProducto
    {
        private readonly FrancaSwContext context;
        public ServiceColorProducto(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<ColoresProducto>> GetColor()
        {
            return await this.context.ColoresProductos.AsNoTracking().ToListAsync();
        }
        public async Task<List<CombosItems>> GetColorForComboBox()
        {
            return await context.ColoresProductos.AsNoTracking().Select<ColoresProducto, CombosItems>(x => x).ToListAsync();
        }

        public async Task<ResultBase> PostColor(ColoresProducto color)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(color);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Color agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el color";
                return resultado;
            }
        }
    }
}
