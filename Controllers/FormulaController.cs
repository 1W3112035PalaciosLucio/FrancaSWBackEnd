using FrancaSW.DataContext;
using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Commands;
using FrancaSW.DTO;
using FrancaSW.Models;
using FrancaSW.Results;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormulaController : ControllerBase
    {
        private readonly IServiceFormula serviceFormula;
        private readonly ISecurityService securityService;

        public FormulaController(IServiceFormula _serviceFormula, ISecurityService _securityService, FrancaSwContext context)
        {
            this.serviceFormula = _serviceFormula;
            this.securityService = _securityService;
        }

        [HttpGet("GetListadoFormula")]
        public async Task<ActionResult> GetListadoFormula()
        {
            return Ok(await this.serviceFormula.GetListadoFormula());
        }

        [HttpGet("GetFormulaById/{id}")]
        public async Task<ActionResult<ResultBase>> GetFormulaById(int id)
        {
            return Ok(await this.serviceFormula.GetFormulaById(id));
        }

        [HttpPost("PostFormula")]
        public async Task<ActionResult<ResultBase>> PostFormula([FromBody] CommandFormula comando)
        {
            Formula form = new Formula();
            form.IdProducto = comando.IdProducto;
            form.IdMateriaPrima = comando.IdMateriaPrima;
            form.CantidadMateriaPrima = comando.CantidadMateriaPrima;

            return Ok(await this.serviceFormula.PostFormula(form));
        }

        [HttpPut("PutProducto")]
        public async Task<ActionResult<ResultBase>> PutProducto([FromBody] DtoFormula dtoFormula)
        {
            if (dtoFormula == null)
            {
                return BadRequest("No se encontró la formula");
            }

            return Ok(await this.serviceFormula.PutFormula(dtoFormula));
        }

    }
}
