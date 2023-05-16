using FrancaSW.Commands.CommandProductos;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisenioProductoController : ControllerBase
    {
        public readonly IServiceDisenioProducto servicio;

        public DisenioProductoController(IServiceDisenioProducto _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetDisenio")]
        public async Task<ActionResult> GetDisenio()
        {
            return Ok(await this.servicio.GetDisenio());
        }

        [HttpPost("PostDisenio")]
        public async Task<ActionResult<ResultBase>> PostDisenio([FromBody] CommandDisenio comando)
        {
            DiseniosProducto disenio = new DiseniosProducto();
            disenio.Descripcion = comando.Descripcion;

            return Ok(await this.servicio.PostDisenio(disenio));
        }
    }
}
