using FrancaSW.DataContext;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarProducto
{
    public class ServiceMedidaProducto:IServiceMedidaProducto
    {
        private readonly FrancaSwContext context;
        public ServiceMedidaProducto(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<MedidasProducto>> GetMedida()
        {
            return await this.context.MedidasProductos.AsNoTracking().ToListAsync();
        }
    }
}
