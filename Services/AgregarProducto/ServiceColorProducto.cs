using FrancaSW.Commands.Combos;
using FrancaSW.DataContext;
using FrancaSW.Models;
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
    }
}
