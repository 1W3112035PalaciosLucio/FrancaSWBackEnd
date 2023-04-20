using FrancaSW.Commands;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services;
using FrancaSW.Services.Security;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IServiceProveedor serviceProveedor;
        private readonly ISecurityService securityService;

        public ProveedorController(IServiceProveedor _serviceProveedor, ISecurityService _securityService)
        {
            this.serviceProveedor = _serviceProveedor;
            this.securityService = _securityService;
        }

        [HttpGet("GetProveedor")]
        public async Task<ActionResult> GetProveedor()
        {
            return Ok(await serviceProveedor.GetProveedor());
        }

        [HttpGet("GetProveedorById/{id}")]
        public async Task<ActionResult<ResultBase>> GetProveedorById(int id)
        {
            return Ok(await serviceProveedor.GetProveedorById(id));
        }

        [HttpGet("GetProveedorByIdd/{id}")]
        public async Task<ActionResult<ResultBase>> GetProveedorByIdd(int id)
        {
            return Ok(await serviceProveedor.GetProveedorByIdd(id));
        }

        [HttpPost("PostProveedor")]
        public async Task<ActionResult<ResultBase>> PostProveedor([FromBody] CommandProveedor comando)
        {
            Proveedore prov = new Proveedore();
            prov.Nombre = comando.Nombre;
            prov.Apellido = comando.Apellido;
            prov.Telefono = comando.Telefono;
            prov.IdLocalidad = comando.IdLocalidad;

            return Ok(await serviceProveedor.PostProveedor(prov));
        }

        [HttpPut("PutProveedor")]
        public async Task<ActionResult<ResultBase>> PutProveedor([FromBody] DtoProveedor dto)
        {

            if (dto == null)
            {
                return BadRequest("El proveedor está vacío");
            }

            return Ok(await this.serviceProveedor.PutProveedor(dto));
        }

        [HttpGet("GetListadoProveedores")]
        public async Task<ActionResult> GetListadoProveedor()
        {
            return Ok(await this.serviceProveedor.GetListadoProveedor());
        }

        [HttpDelete("DesactivarProveedor/{id}")]
        public async Task<ActionResult<ResultBase>> DesactivarProveedor(int id)
        {
            return Ok(await this.serviceProveedor.DesactivarProveedor(id));
        }

        [HttpPut("ActivarProveedor/{id}")]
        public async Task<ActionResult<ResultBase>> ActivarProveedor(int id)
        {
            return Ok(await this.serviceProveedor.ActivarProveedor(id));
        }
    }
}
