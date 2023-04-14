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

        public PrecioMatPrimaProvController(IServicePrecioMatPrimaProv _servicePrecioMatPrimaProv, ISecurityService _securityService, FrancaSwContext context)
        {
            this.servicePrecioMatPrimaProv = _servicePrecioMatPrimaProv;
            this.securityService = _securityService;
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

    }
}
