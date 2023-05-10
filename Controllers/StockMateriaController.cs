using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FrancaSW.Commands;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.DataContext;
using FrancaSW.DTO;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMateriaController : ControllerBase
    {

        private readonly IServiceStockMateriaPrima serviceStockMP;
        private readonly ISecurityService securityService;
        private readonly FrancaSwContext context;

        public StockMateriaController(IServiceStockMateriaPrima _serviceStockMP, ISecurityService _securityService, FrancaSwContext _context)
        {
            this.serviceStockMP = _serviceStockMP;
            this.securityService = _securityService;
            this.context = _context;
        }

        [HttpGet("GetListadoStockMateriaPrima")]
        public async Task<ActionResult> GetListadoStockMateriaPrima()
        {
            return Ok(await this.serviceStockMP.GetListadoStockMateriaPrima());
        }

        [HttpGet("GetStockMateriaPrimaById/{id}")]
        public async Task<ActionResult<ResultBase>> GetStockMateriaPrimaById(int id)
        {
            return Ok(await this.serviceStockMP.GetStockMateriaPrimaById(id));
        }

        [HttpPost("PostStockMateriaPrima")]
        public async Task<ActionResult<ResultBase>> PostStockMP([FromBody] CommandStockMateriaPrima comando)
        {
            StockMateriasPrima stockMp = new StockMateriasPrima();
            stockMp.IdMateriaPrima = comando.IdMateriaPrima;
            stockMp.StockMinimo = comando.StockMinimo;
            stockMp.StockInicial = comando.StockInicial;
            stockMp.Cantidad = comando.Cantidad;
            stockMp.Precio = comando.Precio;
            stockMp.FechaUltimaActualizacion = comando.FechaUltimaActualizacion;
            stockMp.FechaUltimoPrecio = comando.FechaUltimoPrecio;

            return Ok(await serviceStockMP.PostStockMP(stockMp));
        }

        [HttpPut("PutStockMateriaPrima")]
        public async Task<ActionResult<ResultBase>> PutStockMateriaPrima([FromBody] CommandStockMateriaPrima comando)
        {
            try
            {

                var result = await serviceStockMP.PutStockMP(comando);

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
