using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
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

        public async Task<List<PreciosBocha>> GetPrecios()
        {
            return await this.context.PreciosBochas.AsNoTracking().ToListAsync();
        }

        public async Task<ResultBase> PostPrecio(PreciosBocha precio)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(precio);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Precio agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el precio";
                return resultado;
            }
        }
    }
}
