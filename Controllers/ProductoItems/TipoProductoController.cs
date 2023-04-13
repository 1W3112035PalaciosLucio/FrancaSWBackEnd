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
    }
}
