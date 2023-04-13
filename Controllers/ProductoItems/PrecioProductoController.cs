using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecioProductoController:ControllerBase
    {
        public readonly IServicePrecioProducto servicio;

        public PrecioProductoController(IServicePrecioProducto _servicio)
        {
            this.servicio = _servicio;
        }
        
        [HttpGet("GetPrecio")]
        public async Task<ActionResult> GetPrecio()
        {
            return Ok(await this.servicio.GetPrecio());
        }

    }
}
