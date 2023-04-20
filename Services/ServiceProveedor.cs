using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceProveedor : IServiceProveedor
    {
        public readonly FrancaSwContext context;
        public readonly ISecurityService securityService;

        public ServiceProveedor(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        //Lista de proveedores
        public async Task<List<Proveedore>> GetProveedor()
        {
            return await context.Proveedores.AsNoTracking().ToListAsync();
        }
        //Post de proveedor
        public async Task<ResultBase> PostProveedor(Proveedore proveedor)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(proveedor);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Proveedor agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el proveedor";
                return resultado;
            }
        }
        //Put de proveedor
        public async Task<ResultBase> PutProveedor(DtoProveedor dtoProveedor)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                if (dtoProveedor == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El proveedor está vacío";
                    return resultado;
                }

                var proveedor = await context.Proveedores.FindAsync(dtoProveedor.IdProveedor);

                if (proveedor == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un proveedor con el id {dtoProveedor.IdProveedor}";
                    return resultado;
                }

                proveedor.Nombre = dtoProveedor.Nombre;
                proveedor.Apellido = dtoProveedor.Apellido;
                proveedor.Telefono = dtoProveedor.Telefono;
                proveedor.IdLocalidad = dtoProveedor.IdLocalidad;

                context.Update(proveedor);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El proveedor fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el proveedor";
                resultado.Error = ex.ToString();
            }
            return resultado;
        }
        //Lista de proveedor por id
        public async Task<DtoProveedor> GetProveedorById(int id)
        {
            try
            {
                Proveedore proveedor = await context.Proveedores.Where(c => c.IdProveedor.Equals(id)).FirstOrDefaultAsync();
                DtoProveedor dto = new DtoProveedor();
                dto.IdProveedor = proveedor.IdProveedor;
                dto.Nombre = proveedor.Nombre;
                dto.Apellido = proveedor.Apellido;
                dto.Telefono = proveedor.Telefono;
                dto.IdLocalidad = proveedor.IdLocalidad;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DtoProveedorId> GetProveedorByIdd(int id)
        {
            try
            {

                Proveedore proveedor = await context.Proveedores.Include(p => p.IdLocalidadNavigation.IdProvinciaNavigation).Where(c => c.IdProveedor.Equals(id)).FirstOrDefaultAsync();

                if (proveedor == null)
                {
                    // Manejo del caso en que el proveedor no se encuentre
                    return null;
                }

                DtoProveedorId dto = new DtoProveedorId();
                dto.IdProveedor = proveedor.IdProveedor;
                dto.Nombre = proveedor.Nombre;
                dto.Apellido = proveedor.Apellido;
                dto.Telefono = proveedor.Telefono;
                dto.IdLocalidad = proveedor.IdLocalidad;
                dto.IdProvincia = proveedor.IdLocalidadNavigation.IdProvinciaNavigation.IdProvincia;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<DtoListadoProveedor>> GetListadoProveedor()
        {
            return await context.Proveedores.AsNoTracking().
                Select(x => new DtoListadoProveedor
                {
                    IdProveedor = x.IdProveedor,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Telefono = x.Telefono,
                    Localidad = x.IdLocalidadNavigation.Descripcion,
                    Provincia = x.IdLocalidadNavigation.IdProvinciaNavigation.Descripcion,
                    Activo = x.Activo

                }).ToListAsync();
        }

        public async Task<ResultBase> DesactivarProveedor(int id)
        {
            ResultBase resultado = new ResultBase();
            var proveedor = await context.Proveedores.Where(c => c.IdProveedor.Equals(id)).FirstOrDefaultAsync();
            if (proveedor != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El proveedor fue desactivado exitosamente!";
                proveedor.Activo = false;
                context.Update(proveedor);
                await context.SaveChangesAsync();
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al desactivar el proveedor";
                return resultado;
            }
            return resultado;
        }

        public async Task<ResultBase> ActivarProveedor(int id)
        {
            ResultBase resultado = new ResultBase();
            var proveedor = await context.Proveedores.Where(c => c.IdProveedor.Equals(id)).FirstOrDefaultAsync();
            if (proveedor != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El proveedor se activó exitosamente!";
                proveedor.Activo = true;
                context.Update(proveedor);
                await context.SaveChangesAsync();
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al activar el proveedor";
                return resultado;
            }
            return resultado;
        }
    }

}

