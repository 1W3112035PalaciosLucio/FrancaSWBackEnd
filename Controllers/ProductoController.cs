using FrancaSW.Commands;
using FrancaSW.DataContext;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IServiceProducto serviceProducto;
        private readonly ISecurityService securityService;

        public ProductoController(IServiceProducto _serviceProducto, ISecurityService _securityService, FrancaSwContext context)
        {
            this.serviceProducto = _serviceProducto;
            this.securityService = _securityService;
        }

        [HttpGet("GetProducto")]
        public async Task<ActionResult> GetProducto()
        {
            return Ok(await serviceProducto.GetProducto());
        }

        [HttpGet("GetProductoById/{id}")]
        public async Task<ActionResult<ResultBase>> GetProductoById(int id)
        {
            return Ok(await this.serviceProducto.GetProductoById(id));
        }

        [HttpGet("GetProdByCodigo/{id}")]
        public async Task<ActionResult<ResultBase>> GetProdByCodigo(int id)
        {
            return Ok(await this.serviceProducto.GetProdByCodigo(id));
        }

        [HttpPost("PostProducto")]
        public async Task<ActionResult<ResultBase>> PostProducto([FromBody] CommandProducto comando)
        {
            bool productoExistente = await this.serviceProducto.VerificarExistenciaProducto(comando.Codigo);

            if (productoExistente)
            {
                return BadRequest("El producto con el código especificado ya existe en la base de datos.");
            }

            Producto prod = new Producto();
            prod.Nombre = comando.Nombre;
            prod.Codigo = comando.Codigo;
            prod.IdTipoProducto = comando.IdTipoProducto;
            prod.IdColorProducto = comando.IdColorProducto;
            prod.IdMedidaProducto = comando.IdMedidaProducto;
            prod.IdPrecioBocha = comando.IdPreciosBocha;
            prod.IdDisenioProducto = comando.IdDisenioProducto;

            return Ok(await this.serviceProducto.PostProducto(prod));
        }

        [HttpPut("PutProducto")]
        public async Task<ActionResult<ResultBase>> PutProducto([FromBody] DTOProducto dtoProducto)
        {
            if (dtoProducto == null)
            {
                return BadRequest("El objeto DTOProducto está vacío");
            }

            return Ok(await this.serviceProducto.PutProducto(dtoProducto));
        }

        [HttpGet("GetListadoProductos")]
        public async Task<ActionResult> GetListadoProductos()
        {
            return Ok(await this.serviceProducto.GetListadoProductos());
        }

        [HttpDelete("DesactivarProducto/{id}")]
        public async Task<ActionResult<ResultBase>> DesactivarProducto(int id)
        {
            return Ok(await this.serviceProducto.DesactivarProducto(id));
        }

        [HttpPut("ActivarProducto/{id}")]
        public async Task<ActionResult<ResultBase>> ActivarProducto(int id)
        {
            return Ok(await this.serviceProducto.ActivarProducto(id));
        }
    }
}
