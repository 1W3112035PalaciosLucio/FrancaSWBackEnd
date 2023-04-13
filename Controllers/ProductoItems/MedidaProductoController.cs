using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaProductoController : ControllerBase
    {
        public readonly IServiceMedidaProducto servicio;

        public MedidaProductoController (IServiceMedidaProducto _servicio)
        {
            this.servicio = _servicio;
        } 
        
        [HttpGet("GetMedida")]
        public async Task<ActionResult> GetMedida()
        {
            return Ok(await this.servicio.GetMedida());
        }
    }
}
