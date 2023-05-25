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

        [HttpGet("GetListadoReportePrecioMP")]
        public async Task<ActionResult> GetListadoReportePrecioMP()
        {
            return Ok(await this.serviceReporte.GetListadoReportePrecioMP());
        }

        [HttpGet("GetListadoReporteOrdenPendiente")]
        public async Task<ActionResult> GetListadoReporteOrdenPendiente()
        {
            return Ok(await this.serviceReporte.GetListadoReporteOrdenPendiente());
        }

        [HttpGet("GetListadoReporteOrdenPendienteMp")]
        public async Task<ActionResult> GetListadoReporteOrdenPendienteMp()
        {
            return Ok(await this.serviceReporte.GetListadoReporteOrdenPendienteMp());
        }


        [HttpGet("GetListadoReporteMPDisponible")]
        public async Task<ActionResult> GetListadoReporteMPDisponible()
        {
            return Ok(await this.serviceReporte.GetListadoReporteMPDisponible());
        }

        [HttpGet("GetListadoReporteMPStockMinimo")]
        public async Task<ActionResult> GetListadoReporteMPStockMinimo()
        {
            return Ok(await this.serviceReporte.GetListadoReporteMPStockMinimo());
        }
    }
}
