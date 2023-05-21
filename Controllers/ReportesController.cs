using FrancaSW.DataContext;
using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IServiceReportes serviceReporte;
        private readonly ISecurityService securityService;

        public ReportesController(IServiceReportes _serviceReporte, ISecurityService _securityService, FrancaSwContext context)
        {
            this.serviceReporte = _serviceReporte;
            this.securityService = _securityService;
        }

        [HttpGet("GetListadoReporteStockProd")]
        public async Task<ActionResult> GetListadoReporteStockProd()
        {
            return Ok(await this.serviceReporte.GetListadoReporteStockProd());
        }


        [HttpGet("GetListadoReporteStockProd1")]
        public async Task<ActionResult> GetListadoReporteStockProd1()
        {
            return Ok(await this.serviceReporte.GetListadoReporteStockProd1());
        }

        [HttpGet("GetListadoReporteStockMP")]
        public async Task<ActionResult> GetListadoReporteStockMP()
        {
            return Ok(await this.serviceReporte.GetListadoReporteStockMP());
        }
    }
}
