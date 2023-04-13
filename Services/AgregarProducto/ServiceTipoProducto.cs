using FrancaSW.DataContext;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarProducto
{
    public class ServiceTipoProducto: IServiceTipoProducto
    {
        private readonly FrancaSwContext context;
        public ServiceTipoProducto(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<TiposProducto>> GetTipoProducto()
        {
            return await this.context.TiposProductos.AsNoTracking().ToListAsync();
        }
    }
}
