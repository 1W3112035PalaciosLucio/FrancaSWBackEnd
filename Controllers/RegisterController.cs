using FrancaSW.Commands;
using FrancaSW.Models;
using FrancaSW.Results;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IServiceRegister servicio;
        public RegisterController(IServiceRegister _servicio)
        {
            this.servicio = _servicio;
        }
        [HttpPost("PostRegister")]
        public async Task<ActionResult<ResultBase>> PostRegister([FromBody] CommandRegister comando)
        {
            Usuario u = new Usuario();
            byte[] ePass = GetHash(comando.Contrasenia);

            u.Nombre = comando.Nombre;
            u.Apellido = comando.Apellido;
            u.Activo = true;
            u.Email = comando.Email;
            u.Telefono = comando.Telefono;
            u.HashPassword = ePass;
          
           
            

            return Ok(await this.servicio.PostRegister(u));
        }
        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }


    }
}
