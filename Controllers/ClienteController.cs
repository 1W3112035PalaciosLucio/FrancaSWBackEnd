using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Results;
using FrancaSW.Commands;
using FrancaSW.Models;
using FrancaSW.DTO;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IServiceCliente serviceCliente;
        private readonly ISecurityService securityService;

        public ClienteController(IServiceCliente _serviceCliente, ISecurityService _securityService)
        {
            this.serviceCliente = _serviceCliente;
            this.securityService = _securityService;
        }

        [HttpGet("GetCliente")]
        public async Task<ActionResult> GetCliente()
        {
            return Ok(await serviceCliente.GetCliente());
        }

        [HttpGet("GetClienteById/{id}")]
        public async Task<ActionResult<ResultBase>> GetClienteById(int id)
        {
            return Ok(await serviceCliente.GetClienteById(id));
        }

        [HttpPost("PostCliente")]
        public async Task<ActionResult<ResultBase>> PostCliente([FromBody] CommandCliente comando)
        {
            Cliente cli = new Cliente();
            cli.Nombre = comando.Nombre;
            cli.Apellido = comando.Apellido;
            cli.Telefono = comando.Telefono;
            cli.Direccion = comando.Direccion;
            cli.IdLocalidad = comando.IdLocalidad;

            return Ok(await serviceCliente.PostCliente(cli));
        }

        [HttpPut("PutCliente")]
        public async Task<ActionResult<ResultBase>> PutProveedor([FromBody] DtoCliente dto)
        {

            if (dto == null)
            {
                return BadRequest("El cliente está vacío");
            }

            return Ok(await this.serviceCliente.PutCliente(dto));
        }

        [HttpGet("GetListadoCliente")]
        public async Task<ActionResult> GetListadoCliente()
        {
            return Ok(await this.serviceCliente.GetListadoCliente());
        }
    }
}
