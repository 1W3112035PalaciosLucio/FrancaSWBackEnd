using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarProducto
{
    public class ServicePrecioProducto : IServicePrecioProducto
    {
        private readonly FrancaSwContext context;
        public ServicePrecioProducto(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<DtoPrecioBocha>> GetPrecio()
        {
            return await this.context.PreciosBochas.AsNoTracking().Select(x => new DtoPrecioBocha
            {
                idPreciosBocha = x.IdPreciosBocha,
                precio = x.Precio
            }).ToListAsync();
        }
    }
}
