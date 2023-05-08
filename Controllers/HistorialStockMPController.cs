using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Results;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialStockMPController: ControllerBase
    {
        private readonly IServiceHistorialStockMP serviceHistorial;
        private readonly ISecurityService securityService;

        public HistorialStockMPController(IServiceHistorialStockMP _serviceHistorial, ISecurityService _securityService)
        {
            this.serviceHistorial = _serviceHistorial;
            this.securityService = _securityService;
        }

        [HttpGet("GetHistorialStockMP")]
        public async Task<ActionResult> GetHistorialStockMP()
        {
            return Ok(await serviceHistorial.GetListadoHistorialStockMP());
        }


        [HttpGet("GetListaHistStockMPById/{id}")]
        public async Task<ActionResult<ResultBase>> GetListaHistStockMPById(int id)
        {
            return Ok(await serviceHistorial.GetListaHistStockMPById(id));
        }
    }
}
