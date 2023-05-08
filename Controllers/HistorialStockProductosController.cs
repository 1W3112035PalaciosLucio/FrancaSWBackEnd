using FrancaSW.Results;
using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialStockProductosController : ControllerBase
    {
        private readonly IServiceHistorialStockProductos serviceHistorial;
        private readonly ISecurityService securityService;

        public HistorialStockProductosController(IServiceHistorialStockProductos _serviceHistorial, ISecurityService _securityService)
        {
            this.serviceHistorial = _serviceHistorial;
            this.securityService = _securityService;
        }

        [HttpGet("GetListadoHistorialStockProductos")]
        public async Task<ActionResult> GetListadoHistorialStockProductos()
        {
            return Ok(await serviceHistorial.GetListadoHistorialStockProductos());
        }


        [HttpGet("GetListaHistStockProductosById/{id}")]
        public async Task<ActionResult<ResultBase>> GetListaHistStockProductosById(int id)
        {
            return Ok(await serviceHistorial.GetListaHistStockProductosById(id));
        }
    }

}
