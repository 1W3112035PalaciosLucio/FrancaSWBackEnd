using FrancaSW.DataContext;
using FrancaSW.DTO;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceConsultaPrMpProv : IServiceConsultaPrMpProv
    {
        private readonly FrancaSwContext context;

        public ServiceConsultaPrMpProv(FrancaSwContext _context)
        {
            this.context = _context;
        }

        public async Task<List<DtoConsultaPrMPbyProveedor>> ObtenerPreciosPorProveedor(int idProveedor)
        {
            var precios = await (from p in context.Proveedores
                                 join pp in context.PreciosMateriasPrimasProveedores on p.IdProveedor equals pp.IdProveedor
                                 join mp in context.MateriasPrimas on pp.IdMateriaPrima equals mp.IdMateriaPrima
                                 where pp.IdProveedor == idProveedor
                                 select new DtoConsultaPrMPbyProveedor
                                 {
                                     idProveedor = p.IdProveedor,
                                     idMateriaPrima = mp.IdMateriaPrima,
                                     Nombre = p.Nombre,
                                     Apellido = p.Apellido,
                                     Codigo = mp.Codigo,
                                     MateriaPrima = mp.Descripcion,
                                     FechaDesde = pp.FechaVigenciaDesde,
                                     FechaHasta = pp.FechaVigenciaHasta,
                                     Precio = pp.Precio
                                 }).ToListAsync();

            return precios;
        }

        public async Task<List<DtoConsultaPrMPbyProveedor>> ObtenerPreciosPorMateriaPrima(int idMateriaPrima)
        {
            var precios = await (from p in context.Proveedores
                                 join pp in context.PreciosMateriasPrimasProveedores on p.IdProveedor equals pp.IdProveedor
                                 join mp in context.MateriasPrimas on pp.IdMateriaPrima equals mp.IdMateriaPrima
                                 where pp.IdMateriaPrima == idMateriaPrima
                                 select new DtoConsultaPrMPbyProveedor
                                 {
                                     idProveedor = p.IdProveedor,
                                     idMateriaPrima = mp.IdMateriaPrima,
                                     Nombre = p.Nombre,
                                     Apellido = p.Apellido,
                                     Codigo = mp.Codigo,
                                     MateriaPrima = mp.Descripcion,
                                     FechaDesde = pp.FechaVigenciaDesde,
                                     FechaHasta = pp.FechaVigenciaHasta,
                                     Precio = pp.Precio
                                 }).ToListAsync();

            return precios;
        }
    }
}
