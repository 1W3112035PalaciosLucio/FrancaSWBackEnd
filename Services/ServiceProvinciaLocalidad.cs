using FrancaSW.DataContext;
using FrancaSW.Models;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceProvinciaLocalidad : IServiceProvinciaLocalidad
    {
        private readonly FrancaSwContext context;

        public ServiceProvinciaLocalidad(FrancaSwContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<Localidade>> GetLocalidadesByProvincia(int idProvincia)
        {
            return await context.Localidades.Where(l => l.IdProvincia == idProvincia).ToListAsync();
        }

        public async Task<List<Provincia>> GetProvincia()
        {
            return await this.context.Provincias.AsNoTracking().ToListAsync();
        }

        public async Task<List<Localidade>> GetLocalidades()
        {
            return await this.context.Localidades.AsNoTracking().ToListAsync();
        }
    }
}
