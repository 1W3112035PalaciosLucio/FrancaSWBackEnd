using FrancaSW.Commands;
using FrancaSW.DataContext;
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
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IServiceMateriaPrima servicio;
        private readonly ISecurityService securityService;

        public MateriaPrimaController(IServiceMateriaPrima _servicio, FrancaSwContext _context, ISecurityService _securityService)
        {
            this.servicio = _servicio;
            this.securityService = _securityService;
        }
        [HttpGet("GetMateriaPrima")]
        public async Task<ActionResult>GetMateriaPrima()
        {
            return Ok(await this.servicio.GetMateriaPrima());
        }

        [HttpGet("GetMateriaPrimaById/{id}")]
        public async Task<IActionResult> GetMateriaPrimaById(int id)
        {
            return Ok(await servicio.GetMateriaPrimaById(id));
        }

        [HttpPost("PostMateriaPrima")]
        public async Task<ActionResult<ResultBase>> PostMateriaPrima([FromBody] CommandMateriaPrima comando)
        {
            MateriasPrima mp = new MateriasPrima();
            mp.Codigo = comando.Codigo;
            mp.Descripcion = comando.Descripcion;

            return Ok(await this.servicio.PostMateriaPrima(mp));
        }

        [HttpPut("PutMateriaPrima")]
        public async Task<ActionResult<ResultBase>> PutMateriaPrima([FromBody] DTOMateriaPrima comando)
        {
            DTOMateriaPrima mp = new DTOMateriaPrima();
            mp.Codigo= comando.Codigo;
            mp.Descripcion = comando.Descripcion;

            return Ok(await this.servicio.PutMateriaPrima(mp));
        }

    }
}
