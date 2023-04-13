using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorProductoController:ControllerBase
    {
        private IServiceColorProducto servicio;

        public ColorProductoController(IServiceColorProducto _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetColorProducto")]
        public async Task<ActionResult> GetColorProducto()
        {
            return Ok(await this.servicio.GetColor());
        }

        [HttpGet("GetColorProductoForComboBox")]
        public async Task<IActionResult> GetColorProductoForComboBox()
        {
            return Ok(await this.servicio.GetColorForComboBox());
        }
    }
}
