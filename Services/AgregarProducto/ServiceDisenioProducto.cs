using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Results;
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

        public async Task<ResultBase> PostDisenio(DiseniosProducto disenio)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(disenio);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Diseño de producto agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el diseño del producto";
                return resultado;
            }
        }
    }
}
