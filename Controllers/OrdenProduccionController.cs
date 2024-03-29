﻿using FrancaSW.Services.Security;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.DTO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenProduccionController : ControllerBase
    {
        private readonly IServiceOrdenesProduccion serviceOP;
        private readonly ISecurityService securityService;

        public OrdenProduccionController(IServiceOrdenesProduccion _serviceOP, ISecurityService _securityService)
        {
            this.serviceOP = _serviceOP;
            this.securityService = _securityService;
        }

        [HttpGet("GetListadoOrdenProduccion")]
        public async Task<ActionResult> GetListadoOrdenProduccion()
        {
            return Ok(await this.serviceOP.GetListadoOrdenProduccion());

        }
        [HttpGet("GetOrdenProduccionById/{id}")]
        public async Task<ActionResult<ResultBase>> GetOrdenProduccionById(int id)
        {
            return Ok(await this.serviceOP.GetOrdenProduccionById(id));
        }


        [HttpPost("PostOrdenProd")]
        public async Task<ActionResult<ResultBase>> PostOrdenProduccion(DtoOrdenProd ordenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ResultBase resultado = await serviceOP.PostOrdenProd(ordenDto);

            if (resultado.Ok)
            {
                return Ok(resultado);
            }

            return BadRequest(resultado);
        }

        [HttpPut("PutEstado")]
        public async Task<ActionResult<ResultBase>> PutEstado([FromBody] DtoEstadoOrden comando)
        {
            DtoEstadoOrden o = new DtoEstadoOrden();
            o.NumeroOrden = comando.NumeroOrden;
            o.IdEstadoOrdenProduccion = comando.IdEstadoOrdenProduccion;

            return Ok(await this.serviceOP.PutEstado(o));
        }

        [HttpPut("PutOrdenProd")]
        public async Task<ActionResult<ResultBase>> PutOrdenProd([FromBody] DtoOrdenProd orden)
        {
            try
            {
                var result = await serviceOP.PutOrdenProd(orden);

                if (result.Ok)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}