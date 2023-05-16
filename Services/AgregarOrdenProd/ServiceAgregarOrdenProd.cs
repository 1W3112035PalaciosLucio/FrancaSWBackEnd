using FrancaSW.DataContext;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services.AgregarOrdenProd
{
    public class ServiceAgregarOrdenProd : IServiceAgregarOrdenProd
    {
        private readonly FrancaSwContext context;
        public ServiceAgregarOrdenProd(FrancaSwContext _context)
        {
            this.context = _context;
        }
        public async Task<List<Cliente>> GetNCliente()
        {
            return await this.context.Clientes.AsNoTracking().ToListAsync();
        }
        public async Task<List<Cliente>> GetACliente()
        {
            return await this.context.Clientes.AsNoTracking().ToListAsync();
        }
        public async Task<List<Producto>> GetProducto()
        {
            return await this.context.Productos.AsNoTracking().ToListAsync();
        }
        public async Task<List<Usuario>> GetUsuario()
        {
            return await this.context.Usuarios.AsNoTracking().ToListAsync();
        }
        public async Task<List<EstadosOrdenesProduccione>> GetEstado()
        {
            return await this.context.EstadosOrdenesProducciones.AsNoTracking().ToListAsync();
        }
        public async Task<List<EstadosOrdenesProduccione>> GetEstado1()
        {
            var resultado = await this.context.EstadosOrdenesProducciones
                            .Where(estado => estado.IdEstadoOrdenProduccion == 1)
                            .ToListAsync();

            return resultado;
        }
        public async Task<List<EstadosOrdenesProduccione>> GetEstado2()
        {
            var resultado = await this.context.EstadosOrdenesProducciones
                                        .Where(estado => estado.IdEstadoOrdenProduccion != 1)
                                        .ToListAsync();

            return resultado;
        }

    }
}
