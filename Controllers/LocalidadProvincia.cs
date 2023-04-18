using FrancaSW.Models;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrancaSW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalidadProvincia : ControllerBase
    {
        private readonly IServiceProvinciaLocalidad service;

        public LocalidadProvincia(IServiceProvinciaLocalidad _service)
        {
            this.service = _service;
        }

        [HttpGet("{idProvincia}/localidades")]
        public async Task<ActionResult<IEnumerable<Localidade>>> GetLocalidadesByProvincia(int idProvincia)
        {
            if (idProvincia <= 0)
            {
                return BadRequest("El identificador de provincia es inválido.");
            }

            var localidades = await service.GetLocalidadesByProvincia(idProvincia);
            if (localidades == null || !localidades.Any())
            {
                return NotFound();
            }
            return Ok(localidades);
        }

        [HttpGet("GetProvincia")]
        public async Task<ActionResult<Provincia>> GetProvincia()
        {
            return Ok(await this.service.GetProvincia());
        }

        [HttpGet("GetLocalidades")]
        public async Task<ActionResult<Localidade>> GetLocalidades()
        {
            return Ok(await this.service.GetLocalidades());
        }
    }
}
