using FrancaSW.Commands.CommandProductos;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services.AgregarProducto;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers.ProductoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidaProductoController : ControllerBase
    {
        public readonly IServiceMedidaProducto servicio;

        public MedidaProductoController(IServiceMedidaProducto _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpGet("GetMedida")]
        public async Task<ActionResult> GetMedida()
        {
            return Ok(await this.servicio.GetMedida());
        }

        [HttpPost("PostMedida")]
        public async Task<ActionResult<ResultBase>> PostMedida([FromBody] CommandMedida comando)
        {
            MedidasProducto medida = new MedidasProducto();
            medida.Descripcion = comando.Descripcion;

            return Ok(await this.servicio.PostMedida(medida));
        }
    }
}
