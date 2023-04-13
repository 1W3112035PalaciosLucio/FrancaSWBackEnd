using FrancaSW.Commands;
using FrancaSW.DataContext;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrancaSW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly IConfiguration config;
        private readonly IServiceLogin servicio;

        public LoginController(IServiceLogin _servicio, IConfiguration _config, FrancaSwContext context)
        {
            this.servicio = _servicio;
            this.config = _config;
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult> GetUsuarios()
        {
            var get = await this.servicio.GetUsuarios();
            return Ok(get);
        }

        [HttpPost("PostLogin")]
        public async Task<IActionResult> Login([FromBody] CommandLogin comando)
        {
            var result = await this.servicio.Login(comando);
            if (result.Ok)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, result.Email),
                    new Claim(ClaimTypes.Role, string.Join(",", result.Roles))
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(double.Parse(config.GetSection("AppSettings:Expires").Value)),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);
                return Ok(result);
            }
            return Unauthorized(result);

        }
    }
}
