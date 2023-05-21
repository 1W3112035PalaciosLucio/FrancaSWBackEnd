using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FrancaSW.Commands;
using FrancaSW.Comun;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FrancaSW.Services
{
    public class ServiceCatalogo : IServiceCatalogo
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService securityService;
        private readonly Cloudinary cloudinary;
        private readonly IOptions<CloudinarySettings> cloudinarySettings;

        public ServiceCatalogo(FrancaSwContext _context, ISecurityService _securityService,
            IOptions<CloudinarySettings> _cloudinarySettings)
        {
            this.context = _context;
            this.securityService = _securityService;
            this.cloudinarySettings = _cloudinarySettings;
            Account acc = new Account(
                 cloudinarySettings.Value.CloudinaryName,
                 cloudinarySettings.Value.ApiKey,
                 cloudinarySettings.Value.ApiSecret
                );

            cloudinary = new Cloudinary(acc);
        }

        //Listado de catalogo
        public async Task<List<Catalogo>> GetCatalogo()
        {
            return await context.Catalogos.AsNoTracking().ToListAsync();
        }

        public async Task<List<DtoCatalogoCard>> GetCatalogoCard()
        {
            var query = from c in context.Catalogos
                        join p in context.Productos on c.IdProducto equals p.IdProducto
                        orderby p.Codigo
                        select new DtoCatalogoCard
                        {
                            Codigo = p.Codigo,
                            Nombre = p.Nombre,
                            Descripcion = c.Descripcion,
                            Imagen = c.Imagen
                        };
            return await query.ToListAsync();
        }
        
        //Posteo de catalogo
        public async Task<ResultBase> PostCatalogo(Catalogo catalogo)
        {
            ResultBase resultado = new ResultBase();

            // Verificar si el producto existe en la base de datos
            if (await context.Productos.FindAsync(catalogo.IdProducto) == null)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "El producto no existe en la base de datos";
                return resultado;
            }

            try
            {
                await context.AddAsync(catalogo);
                await context.SaveChangesAsync();
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El catalogo fue registrado con exito";
                return resultado;
            }
            catch (Exception)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al registrar el catalogo";
                return resultado;
            }
        }
        public async Task<ResultBase> PosttCatalogo(DtoAgregarCatalogo catalogo)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                if (catalogo == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El catalogo está vacío";
                    return resultado;
                }

                var cat = await context.Catalogos.FindAsync(catalogo.IdProducto);

                if (cat == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un catalogo con el id {catalogo.IdProducto}";
                    return resultado;
                }

                cat.IdProductoNavigation.Activo = catalogo.Activo;
                cat.IdProductoNavigation.Codigo = catalogo.Codigo;
                cat.Descripcion = catalogo.Descripcion;
                cat.IdProductoNavigation.IdColorProducto = catalogo.IdColorProducto;
                cat.IdProductoNavigation.IdDisenioProducto = catalogo.IdDisenioProducto;
                cat.IdProductoNavigation.IdMedidaProducto = catalogo.IdMedidaProducto;
                cat.IdProductoNavigation.IdPrecioBocha = catalogo.IdPreciosBocha;
                cat.IdProducto = catalogo.IdProducto;
                cat.IdProductoNavigation.IdTipoProducto = catalogo.IdTipoProducto;
                cat.Imagen = catalogo.Imagen;
                cat.IdProductoNavigation.Nombre = catalogo.Nombre;


                context.Update(cat);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El catalogo fue creado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al crear el catalogo";
                resultado.Error = ex.ToString();
            }
            return resultado;
        }
        //Modificacion de catalogo
        public async Task<ResultBase> PutCatalogo(CommandCatalogo dtoCatalogo)
        {
            ResultBase resultado = new ResultBase();
            try
            {
                if (dtoCatalogo == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El catalogo está vacío";
                    return resultado;
                }

                var catalogo = await context.Catalogos.FindAsync(dtoCatalogo.IdCatalogo);

                if (catalogo == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un catalogo con el id {dtoCatalogo.IdCatalogo}";
                    return resultado;
                }

                catalogo.Descripcion = dtoCatalogo.Descripcion;
                catalogo.IdProducto = dtoCatalogo.IdProducto;

                var uploadResult = new ImageUploadResult();

                if (dtoCatalogo.Imagen.Length > 0)
                {
                    using (var stream = dtoCatalogo.Imagen.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(dtoCatalogo.Imagen.Name, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                        };

                        uploadResult = cloudinary.Upload(uploadParams);
                    }
                }
                else
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 500;
                    resultado.Message = "Hubo un error al modificar el catalogo";
                }

                catalogo.Imagen = uploadResult.Uri.ToString();
                catalogo.ImagenPublicId = uploadResult.PublicId;


                context.Update(catalogo);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El catalogo fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el catalogo";
                resultado.Error = ex.ToString();
            }
            return resultado;
        }
        //Listado de catalogo por id
        public async Task<DtoCatalogo> GetCatalogoById(int id)
        {
            try
            {
                Catalogo catalogo = await context.Catalogos.Include(p => p.IdProductoNavigation.IdProducto)
                    .Where(c => c.IdCatalogo.Equals(id)).FirstOrDefaultAsync();

                if (catalogo == null)
                {
                    return null;
                }

                DtoCatalogo dto = new DtoCatalogo();
                dto.IdCatalogo = catalogo.IdCatalogo;
                dto.Descripcion = catalogo.Descripcion;
                dto.Imagen = catalogo.Imagen;
                dto.IdProducto = catalogo.IdProductoNavigation.IdProducto;

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<DtoListadoCatalogoProd> GetListaCatalogoProdById(int id)
        {
            try
            {
                return await context.Catalogos.Select(catalogo => new DtoListadoCatalogoProd
                {

                    idCatalogo = catalogo.IdCatalogo,
                    descripcion = catalogo.Descripcion,
                    imagen = catalogo.Imagen,
                    IdProducto = catalogo.IdProductoNavigation.IdProducto,
                    Nombre = catalogo.IdProductoNavigation.Nombre,
                    Codigo = catalogo.IdProductoNavigation.Codigo,
                    IdMedidaProducto = catalogo.IdProductoNavigation.IdMedidaProducto,
                    IdTipoProducto = catalogo.IdProductoNavigation.IdTipoProducto,
                    IdColorProducto = catalogo.IdProductoNavigation.IdColorProducto,
                    IdPreciosBocha = catalogo.IdProductoNavigation.IdPrecioBocha,
                    IdDisenioProducto = catalogo.IdProductoNavigation.IdDisenioProducto
                }).Where(c => c.idCatalogo.Equals(id)).FirstOrDefaultAsync() ?? new DtoListadoCatalogoProd();

            }
            catch (Exception)
            {

                throw;
            }
        }

        //Listado de catalogo completo
        public async Task<List<DtoListadoCatalogo>> GetListadoCatalogo()
        {
            return await context.Catalogos.AsNoTracking().
                OrderBy(x=>x.IdProductoNavigation.Codigo).
                Select(x => new DtoListadoCatalogo
                {
                    IdCatalogo = x.IdCatalogo,
                    Descripcion = x.Descripcion,
                    Imagen = x.Imagen,
                    IdProducto = x.IdProducto,
                    Nombre = x.IdProductoNavigation.Nombre,
                    Codigo = x.IdProductoNavigation.Codigo,
                    TipoProducto = x.IdProductoNavigation.IdTipoProductoNavigation.Descripcion,
                    ColorProducto = x.IdProductoNavigation.IdColorProductoNavigation.Descripcion,
                    DisenioProducto = x.IdProductoNavigation.IdDisenioProductoNavigation.Descripcion,
                    MedidaProducto = x.IdProductoNavigation.IdMedidaProductoNavigation.Descripcion,
                    PrecioBocha = x.IdProductoNavigation.IdPrecioBochaNavigation.Precio
                }).ToListAsync();
        }

        public async Task<DtoCatalogoProd> GetListadoCatalogoProdd(int id)
        {
            try
            {
                return await context.Productos.Select(x => new DtoCatalogoProd
                {
                    IdProducto = x.IdProducto,
                    Nombre = x.Nombre,
                    Codigo = x.Codigo,
                    IdTipoProducto = x.IdTipoProducto,
                    IdColorProducto = x.IdColorProducto,
                    IdDisenioProducto = x.IdDisenioProducto,
                    IdMedidaProducto = x.IdMedidaProducto,
                    IdPreciosBocha = x.IdPrecioBocha
                }).Where(c => c.IdProducto.Equals(id)).FirstOrDefaultAsync() ?? new DtoCatalogoProd();

            }
            catch
            {

                throw;
            }
        }
        public async Task<ResultBase>eliminarCatalogo(int id)
        {
            ResultBase resultado = new ResultBase();
            var catalogo = await context.Catalogos.Where(c => c.IdCatalogo.Equals(id)).FirstOrDefaultAsync();
            if (catalogo != null)
            {
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El catalogo fue eliminado exitosamente!";
                
                context.Remove(catalogo);
                await context.SaveChangesAsync();
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al eliminar el catalogo";
                return resultado;
            }
            return resultado;
        }
    }
}
