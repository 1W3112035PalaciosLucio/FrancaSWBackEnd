using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Commands;
using FrancaSW.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace FrancaSW.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly IConfiguration config;
        private readonly FrancaSwContext context;

        public ServiceLogin(FrancaSwContext _context, IConfiguration _config)
        {
            this.context = _context;
            this.config = _config;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await context.Usuarios.AsNoTracking().ToListAsync();
        }
        public async Task<CommandLogin> Login([FromBody] CommandLogin comando)
        {
            CommandLogin emailPass = new CommandLogin();
            try
            {
                byte[] ePass = GetHash(comando.Password);
                var activo = await context.Usuarios.FirstOrDefaultAsync(c => c.Activo);

                emailPass = await context.Usuarios
                    .Include(x => x.RolesUsuarios)
                    .ThenInclude(x => x.IdRolNavigation)
                    .FirstOrDefaultAsync(c => c.Email == comando.Email && c.HashPassword == ePass) ?? new CommandLogin();
                if (emailPass != null)
                {



                    if (emailPass.Activo && activo != null)
                    {
                        emailPass.Ok = true;
                        emailPass.StateCode = 200;
                        emailPass.Error = "Es activo y valido";
                        return emailPass;
                    }
                    else
                    {
                        emailPass.Ok = false;
                        emailPass.StateCode = 400;
                        emailPass.Error = "El email no esta activo";
                        return emailPass;
                    }
                }
                else
                {
                    emailPass.Ok = false;
                    emailPass.StateCode = 400;
                    emailPass.Error = "El email o contraseña no existe";
                    return emailPass;
                }

            }
            catch (Exception ex)
            {

                emailPass.Ok = false;
                emailPass.StateCode = 400;
                emailPass.Error = ex.Message;
                return emailPass;
            }
        }

        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }


    }
}
