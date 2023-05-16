using FrancaSW.Commands.CommandProductos;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        public readonly IServiceTipoProducto servicio;

        public TipoProductoController(IServiceTipoProducto _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetTipoProducto")]
        public async Task<ActionResult> GetTipoProducto()
        {
            return Ok(await this.servicio.GetTipoProducto());
        }

        [HttpPost("PostTipoProd")]
        public async Task<ActionResult<ResultBase>> PostTipoProd([FromBody] CommandTipoProd comando)
        {
            TiposProducto tipo = new TiposProducto();
            tipo.Descripcion = comando.Descripcion;

            return Ok(await this.servicio.PostTipoProd(tipo));
        }
    }
}
