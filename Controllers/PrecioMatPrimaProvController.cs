using FrancaSW.DataContext;
using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Results;
using FrancaSW.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using FrancaSW.DTO;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecioMatPrimaProvController : ControllerBase
    {
        private readonly IServicePrecioMatPrimaProv servicePrecioMatPrimaProv;
        private readonly ISecurityService securityService;

        private readonly IServiceConsultaPrMpProv serviceConsultaPrMpProv;

        public PrecioMatPrimaProvController(IServicePrecioMatPrimaProv _servicePrecioMatPrimaProv, 
            ISecurityService _securityService, FrancaSwContext context, IServiceConsultaPrMpProv _serviceConsultaPrMpProv)
        {
            this.servicePrecioMatPrimaProv = _servicePrecioMatPrimaProv;
            this.securityService = _securityService;
            this.serviceConsultaPrMpProv = _serviceConsultaPrMpProv;
        }

        [HttpPost("PostMatPrimaProv")]
        public async Task<ActionResult<ResultBase>> PostPrecioMatPrimaProv([FromBody] DtoPrecioMatPrimaProv comando)
        {
            PreciosMateriasPrimasProveedore mpp = new PreciosMateriasPrimasProveedore();
            mpp.IdProveedor = comando.IdProveedor;
            mpp.IdMateriaPrima = comando.IdMateriaPrima;
            mpp.FechaVigenciaDesde = comando.FechaVigenciaDesde;
            mpp.FechaVigenciaHasta = comando.FechaVigenciaHasta;
            mpp.Precio = comando.Precio;

            return Ok(await this.servicePrecioMatPrimaProv.PostPrecioMatPrimaProv(mpp));
        }

        [HttpGet("GetPreciosMateriasPrimasProveedores")]
        public async Task<ActionResult<List<DtoPrecioMatPrimaProv>>> GetPreciosMateriasPrimasProveedores()
        {
            return Ok(await this.servicePrecioMatPrimaProv.GetListaPrecioMatPrimaProv());
        }
        
        [HttpPut("PutPreciosMateriasPrimasProveedores")]
        public async Task<ActionResult<ResultBase>> PutPrecioMatPrimaProv([FromBody] DtoPrecioMatPrimaProv dtoPrecio)
        {
            if (dtoPrecio == null)
            {
                return BadRequest("El objeto DTOPrecioMatPrimaProv está vacío");
            }

            return Ok(await this.servicePrecioMatPrimaProv.PutPrecioMatPrimaProv(dtoPrecio));
        }

        [HttpGet("GetPrecioMatPrimaProvById/{id}")]
        public async Task<ActionResult<ResultBase>> GetPrecioMatPrimaProvById(int id)
        {
            return Ok(await this.servicePrecioMatPrimaProv.GetPrecioMatPrimaProvById(id));
        }

        [HttpGet("GetConsultaMpByProveedor/{idProveedor}")]
        public async Task<ActionResult<List<DtoConsultaPrMPbyProveedor>>> GetConsultaMpByProveedor(int idProveedor)
        {
            var precios = await serviceConsultaPrMpProv.ObtenerPreciosPorProveedor(idProveedor);

            if (precios == null || precios.Count == 0)
            {
                return NotFound();
            }

            return Ok(precios);
        }

        [HttpGet("GetConsultaMpByMateriaPrima/{idMateriaPrima}")]
        public async Task<ActionResult<List<DtoConsultaPrMPbyProveedor>>> GetConsultaMpByMateriaPrima(int idMateriaPrima)
        {
            var precios = await serviceConsultaPrMpProv.ObtenerPreciosPorMateriaPrima(idMateriaPrima);

            if (precios == null || precios.Count == 0)
            {
                return NotFound();
            }

            return Ok(precios);
        }

    }
}
