using FrancaSW.Commands;
using FrancaSW.Commands.CommandProductos;
using FrancaSW.Models;
using FrancaSW.Results;
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

        [HttpGet("GetPrecios")]
        public async Task<ActionResult> GetPrecios()
        {
            return Ok(await this.servicio.GetPrecios());
        }

        [HttpPost("PostPrecio")]
        public async Task<ActionResult<ResultBase>> PostPrecio([FromBody] CommandPrecioProd comando)
        {
            PreciosBocha precio = new PreciosBocha();
            precio.FechaVigenciaDesde = comando.FechaVigenciaDesde;
            precio.FehcaVigenciaHasta = comando.FehcaVigenciaHasta;
            precio.Precio = comando.Precio;


            return Ok(await this.servicio.PostPrecio(precio));
        }

    }
}
