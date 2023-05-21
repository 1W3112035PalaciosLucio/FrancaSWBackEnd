using FrancaSW.Comun;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrancaSW.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public readonly FrancaSwContext context;
        public readonly ISecurityService securityService;

        public ServiceProducto(FrancaSwContext _context, ISecurityService _securityService)
        {
            this.context = _context;
            this.securityService = _securityService;
        }

        //Listado de productos
        public async Task<List<Producto>> GetProducto()
        {
            return await context.Productos.AsNoTracking().ToListAsync();
        }

        public async Task<List<DtoListadoProductos>> GetListadoProductos()
        {
            return await context.Productos.AsNoTracking().
                OrderBy(x=>x.Codigo).
                Select(x => new DtoListadoProductos
                {
                    idProducto = x.IdProducto,
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    ColorProducto = x.IdColorProductoNavigation.Descripcion,
                    DisenioProducto = x.IdDisenioProductoNavigation.Descripcion,
                    TipoProducto = x.IdTipoProductoNavigation.Descripcion,
                    MedidaProducto = x.IdMedidaProductoNavigation.Descripcion,
                    PrecioBocha = x.IdPrecioBochaNavigation.Precio,
                    Activo = (bool)x.Activo

                }).ToListAsync();
        }
        //Posteo de productos
        public async Task<ResultBase> PostProducto(Producto p)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                await context.AddAsync(p);

                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Producto agregado correctamente";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al cargar el producto";
                return resultado;
            }
        }
        //Modificacion de producto
        public async Task<ResultBase> PutProducto(DTOProducto dtoProducto)
        {
            ResultBase resultado = new ResultBase();

            try
            {
                if (dtoProducto == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El objeto DTOProducto está vacío";
                    return resultado;
                }

                var producto = await context.Productos.FindAsync(dtoProducto.IdProducto);

                if (producto == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un producto con el id {dtoProducto.IdProducto}";
                    return resultado;
                }

                producto.Nombre = dtoProducto.Nombre;
                producto.Codigo = dtoProducto.Codigo;
                producto.IdMedidaProducto = dtoProducto.IdMedidaProducto;
                producto.IdDisenioProducto = dtoProducto.IdDisenioProducto;
                producto.IdPrecioBocha = dtoProducto.IdPreciosBocha;
                producto.IdTipoProducto = dtoProducto.IdTipoProducto;
                producto.IdColorProducto = dtoProducto.IdColorProducto;
                producto.Activo = dtoProducto.Activo;

                context.Update(producto);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El producto fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el producto";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }

        //Listado de productos por id
        public async Task<DTOProducto> GetProductoById(int id)
        {
            try
            {
                Producto producto = await context.Productos.Where(c => c.IdProducto.Equals(id)).FirstOrDefaultAsync();
                DTOProducto dto = new DTOProducto();
                dto.IdProducto = producto.IdProducto;
                dto.Codigo = producto.Codigo;
                dto.Nombre = producto.Nombre;
                dto.IdTipoProducto = producto.IdTipoProducto;
                dto.IdColorProducto = producto.IdColorProducto;
                dto.IdMedidaProducto = producto.IdMedidaProducto;
                dto.IdPreciosBocha = producto.IdPrecioBocha;
                dto.IdDisenioProducto = producto.IdDisenioProducto;
                dto.Activo = producto.Activo;
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<DTOProducto> GetProdByCodigo(int id)
        {
            try
            {
                Producto producto = await context.Productos.Where(c => c.IdProducto.Equals(id)).FirstOrDefaultAsync();
                DTOProducto dto = new DTOProducto();
                dto.IdProducto = producto.IdProducto;
                dto.Codigo = producto.Codigo;
                dto.Nombre = producto.Nombre;
                dto.IdTipoProducto = producto.IdTipoProducto;
                dto.IdColorProducto = producto.IdColorProducto;
                dto.IdMedidaProducto = producto.IdMedidaProducto;
                dto.IdPreciosBocha = producto.IdPrecioBocha;
                dto.IdDisenioProducto = producto.IdDisenioProducto;
                dto.Activo = producto.Activo;
                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Desactivar producto
        public async Task<ResultBase> DesactivarProducto(int id)
        {
            ResultBase resultado = new ResultBase();
            var producto = await context.Productos.Where(c => c.Codigo.Equals(id)).FirstOrDefaultAsync();
            if (producto != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El producto fue desactivado exitosamente!";
                producto.Activo = false;
                context.Update(producto);
                await context.SaveChangesAsync();
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al desactivar el producto";
                return resultado;
            }
            return resultado;
        }

        //Activar producto
        public async Task<ResultBase> ActivarProducto(int id)
        {
            ResultBase resultado = new ResultBase();
            var producto = await context.Productos.Where(c => c.Codigo.Equals(id)).FirstOrDefaultAsync();
            if (producto != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El producto se activo exitosamente!";
                producto.Activo = true;
                context.Update(producto);
                await context.SaveChangesAsync();
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al activar el producto";
                return resultado;
            }
            return resultado;
        }
    }
}
