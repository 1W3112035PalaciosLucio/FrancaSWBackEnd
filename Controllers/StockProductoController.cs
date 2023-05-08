using FrancaSW.DataContext;
using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Results;
using FrancaSW.Commands;
using FrancaSW.Models;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProductoController : ControllerBase
    {
        private readonly IServiceStockProductos serviceStockProductos;
        private readonly ISecurityService securityService;
        private readonly FrancaSwContext context;

        public StockProductoController(IServiceStockProductos _serviceStockProductos, ISecurityService _securityService,
            FrancaSwContext _context)
        {
            this.serviceStockProductos = _serviceStockProductos;
            this.securityService = _securityService;
            this.context = _context;
        }

        [HttpGet("GetListadoStockProducto")]
        public async Task<ActionResult> GetListadoStockProducto()
        {
            return Ok(await this.serviceStockProductos.GetListadoStockProducto());
        }

        [HttpGet("GetStockProductoById/{id}")]
        public async Task<ActionResult<ResultBase>> GetStockProductoById(int id)
        {
            return Ok(await this.serviceStockProductos.GetStockProductoById(id));
        }

        [HttpPost("PostStockProducto")]
        public async Task<ActionResult<ResultBase>> PostStockProducto([FromBody] CommandStockProductos comando)
        {
            StockProducto stockP = new StockProducto();
            stockP.IdProducto = comando.IdProducto;
            stockP.Cantidad = comando.Cantidad;
            stockP.FechaUltimaActualizacion = comando.FechaUltimaActualizacion;

            return Ok(await serviceStockProductos.PostStockProducto(stockP));
        }

        [HttpPut("PutStockProducto")]
        public async Task<ActionResult<ResultBase>> PutStockProducto([FromBody] CommandStockProductos comando)
        {
            try
            {
                var result = await  serviceStockProductos.PutStockProducto(comando);

                if (result.Ok)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception)
            {
                var resultado = new ResultBase
                {
                    Ok = false,
                    CodigoEstado = 400,
                    Message = "Error al actualizar el stock"
                };
                return BadRequest(resultado);
            }
        }
    }
}
