using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisenioProductoController : ControllerBase
    {
        public readonly IServiceDisenioProducto servicio;

        public DisenioProductoController (IServiceDisenioProducto _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetDisenio")]
        public async Task<ActionResult> GetDisenio()
        {
            return Ok(await this.servicio.GetDisenio());
        }

    }
}
