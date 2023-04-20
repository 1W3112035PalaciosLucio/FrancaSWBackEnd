using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FrancaSW.Services
{
    public class ServicePrecioMatPrimaProv : IServicePrecioMatPrimaProv
    {
        private readonly FrancaSwContext context;

        public ServicePrecioMatPrimaProv(FrancaSwContext _context)
        {
            this.context = _context;
        }

        public async Task<ResultBase> PostPrecioMatPrimaProv(PreciosMateriasPrimasProveedore mp)
        {

            ResultBase resultado = new ResultBase();

            try
            {
                // Verificar que exista el proveedor
                var proveedorExistente = await context.Proveedores.FindAsync(mp.IdProveedor);
                if (proveedorExistente == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Proveedor no encontrado.";
                    return resultado;
                }

                // Verificar que exista la materia prima
                var materiaPrimaExistente = await context.MateriasPrimas.FindAsync(mp.IdMateriaPrima);
                if (materiaPrimaExistente == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "Materia prima no encontrada.";
                    return resultado;
                }

                // Agregar el nuevo precio
                await context.PreciosMateriasPrimasProveedores.AddAsync(mp);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Precio de materia prima agregado correctamente.";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al agregar el precio de materia prima.";
                return resultado;
            }
        }

        public async Task<List<DtoListaPrMatPrProv>> GetListaPrecioMatPrimaProv()
        {
            var listaPrecios = await context.PreciosMateriasPrimasProveedores.AsNoTracking()
                .Include(x => x.IdProveedorNavigation)
                .Include(x => x.IdMateriaPrimaNavigation)
                .Select(x => new DtoListaPrMatPrProv
                {
                    IdPreciosMateriaPrimaProveedor = x.IdPreciosMateriaPrimaProveedor,
                    IdProveedor = x.IdProveedor,
                    Nombre = x.IdProveedorNavigation.Nombre,
                    IdMateriaPrima = x.IdMateriaPrima,
                    NombreMateriaPrima = x.IdMateriaPrimaNavigation.Descripcion,
                    FechaVigenciaDesde = x.FechaVigenciaDesde,
                    FechaVigenciaHasta = x.FechaVigenciaHasta,
                    Precio = x.Precio,
                    Apellido = x.IdProveedorNavigation.Apellido,
                    Telefono = x.IdProveedorNavigation.Telefono
                }).ToListAsync();

            return listaPrecios;
        }

        public async Task<ResultBase> PutPrecioMatPrimaProv(DtoPrecioMatPrimaProv dtoPrecio)
        {
            ResultBase resultado = new ResultBase();

            try
            {
                if (dtoPrecio == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El objeto está vacío";
                    return resultado;
                }

                var precioMatPrimaProv = await context.PreciosMateriasPrimasProveedores.FindAsync(dtoPrecio.IdPreciosMateriaPrimaProveedor);

                if (precioMatPrimaProv == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un precio de materia prima proveedor con el idProveedor {dtoPrecio.IdProveedor} y el idMateriaPrima {dtoPrecio.IdMateriaPrima}";
                    return resultado;
                }

                // Validaciones de modelo
                if (dtoPrecio.FechaVigenciaDesde == null || dtoPrecio.FechaVigenciaDesde == DateTime.MinValue)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "La fecha de vigencia desde es requerida";
                    return resultado;
                }

                if (dtoPrecio.FechaVigenciaHasta == null || dtoPrecio.FechaVigenciaHasta == DateTime.MinValue)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "La fecha de vigencia hasta es requerida";
                    return resultado;
                }

                if (dtoPrecio.FechaVigenciaDesde > dtoPrecio.FechaVigenciaHasta)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "La fecha de vigencia desde no puede ser mayor que la fecha de vigencia hasta";
                    return resultado;
                }

                if (dtoPrecio.Precio <= 0)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El precio debe ser mayor que cero";
                    return resultado;
                }


                precioMatPrimaProv.FechaVigenciaDesde = dtoPrecio.FechaVigenciaDesde;
                precioMatPrimaProv.FechaVigenciaHasta = dtoPrecio.FechaVigenciaHasta;
                precioMatPrimaProv.Precio = dtoPrecio.Precio;

                context.Update(precioMatPrimaProv);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El precio de materia prima proveedor fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el precio de materia prima proveedor";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }

        public async Task<DtoListaPrMatPrProv> GetPrecioMatPrimaProvById(int id)
        {
            try
            {
                PreciosMateriasPrimasProveedore var = await context.PreciosMateriasPrimasProveedores
                    .Include(p => p.IdProveedorNavigation)
                    .Include(p => p.IdMateriaPrimaNavigation)
                    .Where(p => p.IdPreciosMateriaPrimaProveedor == id)
                    .FirstOrDefaultAsync();

                if (var == null)
                    return null;

                DtoListaPrMatPrProv dto = new DtoListaPrMatPrProv();
                dto.IdPreciosMateriaPrimaProveedor = var.IdPreciosMateriaPrimaProveedor;
                dto.IdProveedor = var.IdProveedor;
                dto.Nombre = var.IdProveedorNavigation.Nombre;
                dto.IdMateriaPrima = var.IdMateriaPrima;
                dto.NombreMateriaPrima = var.IdMateriaPrimaNavigation.Descripcion;
                dto.FechaVigenciaDesde = var.FechaVigenciaDesde;
                dto.FechaVigenciaHasta = var.FechaVigenciaHasta;
                dto.Precio = var.Precio;
                dto.Apellido = var.IdProveedorNavigation.Apellido;
                dto.Telefono = var.IdProveedorNavigation.Telefono;

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DtoListaPrecioMatPrProv> GetPrecioMatPrimaProvByIdd(int id)
        {
            try
            {
                PreciosMateriasPrimasProveedore var = await context.PreciosMateriasPrimasProveedores
                 .Include(p => p.IdProveedorNavigation)
                     .ThenInclude(p => p.IdLocalidadNavigation)
                         .ThenInclude(l => l.IdProvinciaNavigation)
                 .Include(p => p.IdMateriaPrimaNavigation)
                 .Where(p => p.IdPreciosMateriaPrimaProveedor == id)
                 .FirstOrDefaultAsync();

                if (var == null)
                    return null;

                DtoListaPrecioMatPrProv dto = new DtoListaPrecioMatPrProv();
                dto.IdPreciosMateriaPrimaProveedor = var.IdPreciosMateriaPrimaProveedor;
                dto.IdProveedor = var.IdProveedor;
                dto.Nombre = var.IdProveedorNavigation.Nombre;
                dto.IdMateriaPrima = var.IdMateriaPrima;
                dto.NombreMateriaPrima = var.IdMateriaPrimaNavigation.Descripcion;
                dto.FechaVigenciaDesde = var.FechaVigenciaDesde;
                dto.FechaVigenciaHasta = var.FechaVigenciaHasta;
                dto.Precio = var.Precio;
                dto.Apellido = var.IdProveedorNavigation.Apellido;
                dto.Telefono = var.IdProveedorNavigation.Telefono;
                dto.Activo = var.IdProveedorNavigation.Activo;
                dto.Localidad = var.IdProveedorNavigation.IdLocalidadNavigation.Descripcion;
                dto.Provincia = var.IdProveedorNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Descripcion;

                return dto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DtoListaPrecioMatPrProv>> GetAllPrecioMatPrimaProv()
        {
            try
            {
                List<PreciosMateriasPrimasProveedore> precios = await context.PreciosMateriasPrimasProveedores
                .Include(p => p.IdProveedorNavigation)
                .ThenInclude(p => p.IdLocalidadNavigation)
                .ThenInclude(l => l.IdProvinciaNavigation)
                .Include(p => p.IdMateriaPrimaNavigation)
                .ToListAsync();

                if (precios == null || precios.Count == 0)
                    return null;

                List<DtoListaPrecioMatPrProv> dtos = new List<DtoListaPrecioMatPrProv>();
                foreach (PreciosMateriasPrimasProveedore var in precios)
                {
                    DtoListaPrecioMatPrProv dto = new DtoListaPrecioMatPrProv();
                    dto.IdPreciosMateriaPrimaProveedor = var.IdPreciosMateriaPrimaProveedor;
                    dto.IdProveedor = var.IdProveedor;
                    dto.Nombre = var.IdProveedorNavigation.Nombre;
                    dto.IdMateriaPrima = var.IdMateriaPrima;
                    dto.NombreMateriaPrima = var.IdMateriaPrimaNavigation.Descripcion;
                    dto.FechaVigenciaDesde = var.FechaVigenciaDesde;
                    dto.FechaVigenciaHasta = var.FechaVigenciaHasta;
                    dto.Precio = var.Precio;
                    dto.Apellido = var.IdProveedorNavigation.Apellido;
                    dto.Telefono = var.IdProveedorNavigation.Telefono;
                    dto.Activo = var.IdProveedorNavigation.Activo;
                    dto.Localidad = var.IdProveedorNavigation.IdLocalidadNavigation.Descripcion;
                    dto.Provincia = var.IdProveedorNavigation.IdLocalidadNavigation.IdProvinciaNavigation.Descripcion;
                    dtos.Add(dto);
                }

                return dtos;

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
