using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceCliente : IServiceCliente
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;

        public ServiceCliente(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        //Listado de clientes
        public async Task<List<Cliente>> GetCliente()
        {
            return await context.Clientes.AsNoTracking().ToListAsync();
        }
        //Posteo de clientes
        public async Task<ResultBase> PostCliente(Cliente cliente)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(cliente);
                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El cliente fue registrado con exito";
                return resultado;

            }
            catch (Exception)
            {

                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al registrar el cliente";
                return resultado;
            }
        }
        //Modificacion de materias primas
        public async Task<ResultBase> PutCliente(DtoCliente dtoCliente)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                if (dtoCliente == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El cliente está vacío";
                    return resultado;
                }

                var cliente = await context.Clientes.FindAsync(dtoCliente.IdCliente);

                if (cliente == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un cliente con el id {dtoCliente.IdCliente}";
                    return resultado;
                }

                cliente.Nombre = dtoCliente.Nombre;
                cliente.Apellido = dtoCliente.Apellido;
                cliente.Telefono = dtoCliente.Telefono;
                cliente.Direccion = dtoCliente.Direccion;
                cliente.IdLocalidad = dtoCliente.IdLocalidad;


                context.Update(cliente);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El cliente fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el cliente";
                resultado.Error = ex.ToString();
            }
            return resultado;
        }
        //Listado de clientes por id
        public async Task<DtoClienteId> GetClienteById(int id)
        {
            try
            {
                Cliente cliente = await context.Clientes.Include(p => p.IdLocalidadNavigation.IdProvinciaNavigation)
                    .Where(c => c.IdCliente.Equals(id)).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return null;
                }

                DtoClienteId dto = new DtoClienteId();
                dto.IdCliente = cliente.IdCliente;
                dto.Nombre = cliente.Nombre;
                dto.Apellido = cliente.Apellido;
                dto.Telefono = cliente.Telefono;
                dto.Direccion = cliente.Direccion;
                dto.IdLocalidad = cliente.IdLocalidad;
                dto.IdProvincia = cliente.IdLocalidadNavigation.IdProvinciaNavigation.IdProvincia;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<DtoListadoCliente>> GetListadoCliente()
        {
            return await context.Clientes.AsNoTracking().
                Select(x => new DtoListadoCliente
                {
                    IdCliente = x.IdCliente,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Telefono = x.Telefono,
                    Localidad = x.IdLocalidadNavigation.Descripcion,
                    Provincia = x.IdLocalidadNavigation.IdProvinciaNavigation.Descripcion,
                    Direccion = x.Direccion

                }).ToListAsync();
        }

    }
}

