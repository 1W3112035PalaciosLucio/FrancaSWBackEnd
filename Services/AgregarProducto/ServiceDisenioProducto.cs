using FrancaSW.DataContext;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarProducto
{
    public class ServiceDisenioProducto:IServiceDisenioProducto
    {
        private readonly FrancaSwContext context;
        public ServiceDisenioProducto(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<DiseniosProducto>> GetDisenio()
        {
            return await this.context.DiseniosProductos.AsNoTracking().ToListAsync();
        }
    }
}
