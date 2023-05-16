using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Results;
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

        public async Task<ResultBase> PostMedida(MedidasProducto medida)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(medida);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Medida de producto agregada correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar la medida del producto";
                return resultado;
            }
        }

    }
}
