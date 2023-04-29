using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FrancaSW.Commands;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using FrancaSW.Comun;
using Microsoft.Extensions.Options;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IServiceCatalogo serviceCatalogo;
        private readonly ISecurityService securityService;
        private readonly Cloudinary cloudinary;
        private readonly IOptions<CloudinarySettings> cloudinarySettings;

        public CatalogoController(IServiceCatalogo _serviceCataloo, ISecurityService _securityService,
             IOptions<CloudinarySettings> _cloudinarySettings)
        {
            this.serviceCatalogo = _serviceCataloo;
            this.securityService = _securityService;

            this.cloudinarySettings = _cloudinarySettings;
            Account acc = new Account(
                 cloudinarySettings.Value.CloudinaryName,
                 cloudinarySettings.Value.ApiKey,
                 cloudinarySettings.Value.ApiSecret
                );

            cloudinary = new Cloudinary(acc);
        }

        [HttpGet("GetCatalogo")]
        public async Task<ActionResult> GetCatalogo()
        {
            return Ok(await serviceCatalogo.GetCatalogo());
        }

        [HttpGet("GetCatalogoCard")]
        public async Task<ActionResult> GetCatalogoCard()
        {
            return Ok(await serviceCatalogo.GetCatalogoCard());
        }
        
        [HttpGet("GetCatalogoById/{id}")]
        public async Task<ActionResult<ResultBase>> GetCatalogoById(int id)
        {
            return Ok(await serviceCatalogo.GetCatalogoById(id));
        }

        [HttpGet("GetListaCatalogoProdById/{id}")]
        public async Task<ActionResult<ResultBase>> GetListaCatalogoProdById(int id)
        {
            return Ok(await serviceCatalogo.GetListaCatalogoProdById(id));
        }

        [HttpPost("PostCatalogo")]
        public async Task<ActionResult<ResultBase>> PostCatalogo([FromForm] CommandCatalogo comando)
        {
            Catalogo catalogo = new Catalogo();
            catalogo.Descripcion = comando.Descripcion;
            catalogo.IdProducto = comando.IdProducto;


            var uploadResult = new ImageUploadResult();

            if (comando.Imagen.Length > 0)
            {
                using (var stream = comando.Imagen.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(comando.Imagen.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }
            }
            else
            {
                return BadRequest("Please provide a photo!");
            }

            catalogo.Imagen = uploadResult.Uri.ToString();
            catalogo.ImagenPublicId = uploadResult.PublicId;

            return Ok(await serviceCatalogo.PostCatalogo(catalogo));
        }

        [HttpPut("PutCatalogo")]
        public async Task<ActionResult<ResultBase>> PutCatalogo([FromForm] CommandCatalogo dto)
        {

            if (dto == null)
            {
                return BadRequest("El catalogo está vacío");
            }

            return Ok(await this.serviceCatalogo.PutCatalogo(dto));
        }

        [HttpGet("GetListadoCatalogo")]
        public async Task<ActionResult> GetListadoCliente()
        {
            return Ok(await this.serviceCatalogo.GetListadoCatalogo());
        }

        [HttpGet("GetListadoCatalogoProdd/{id}")]
        public async Task<ActionResult<ResultBase>> GetListadoCatalogoProdd(int id)
        {
            return Ok(await this.serviceCatalogo.GetListadoCatalogoProdd(id));
        }
    }
}
