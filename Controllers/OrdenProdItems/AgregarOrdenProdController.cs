using FrancaSW.Services.AgregarOrdenProd;
using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.OrdenProdItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgregarOrdenProdController : ControllerBase
    {
        private IServiceAgregarOrdenProd servicio;

        public AgregarOrdenProdController(IServiceAgregarOrdenProd _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetNCliente")]
        public async Task<ActionResult> GetNCliente()
        {
            return Ok(await this.servicio.GetNCliente());
        }
        [HttpGet("GetACliente")]
        public async Task<ActionResult> GetACliente()
        {
            return Ok(await this.servicio.GetACliente());
        }
        [HttpGet("GetProducto")]
        public async Task<ActionResult> GetProducto()
        {
            return Ok(await this.servicio.GetProducto());
        }
        [HttpGet("GetUsuario")]
        public async Task<ActionResult> GetUsuario()
        {
            return Ok(await this.servicio.GetUsuario());
        }
        [HttpGet("GetEstado")]
        public async Task<ActionResult> GetEstado()
        {
            return Ok(await this.servicio.GetEstado());
        }

        [HttpGet("GetEstado1")]
        public async Task<ActionResult> GetEstado1()
        {
            return Ok(await this.servicio.GetEstado1());
        }
        [HttpGet("GetEstado2")]
        public async Task<ActionResult> GetEstado2()
        {
            return Ok(await this.servicio.GetEstado2());
        }
    }
}
