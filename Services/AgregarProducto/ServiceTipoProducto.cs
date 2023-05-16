using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Results;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarProducto
{
    public class ServiceTipoProducto : IServiceTipoProducto
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

        public async Task<ResultBase> PostTipoProd(TiposProducto tipo)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(tipo);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Tipo de producto agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el tipo de producto";
                return resultado;
            }
        }
    }
}
