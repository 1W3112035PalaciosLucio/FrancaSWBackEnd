using FrancaSW.DataContext;
using FrancaSW.Models;
using FrancaSW.Results;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using FrancaSW.Services.Security;
using FrancaSW.Comun;
using FrancaSW.Services;

namespace FrancaSW.Services
{
    public class ServiceRegister : IServiceRegister
    {
        private readonly FrancaSwContext context;
        private readonly ISecurityService _securityService;

        public ServiceRegister(FrancaSwContext _context, ISecurityService securityService)
        {
            this.context = _context;
            this._securityService = securityService;
        }

        public async Task<ResultBase> PostRegister(Usuario u)
        {
            ResultBase resultado = new ResultBase();

            if (this.ValidarMail(u.Email))
            {
                if (this.validarExpresion(u.Email))
                {
                    try
                    {
                        await context.AddAsync(u);
                        await context.SaveChangesAsync();
                        resultado.Ok = true;
                        resultado.CodigoEstado = 200;
                        return resultado;
                    }
                    catch (Exception)
                    {

                        resultado.Ok = false;
                        resultado.CodigoEstado = 400;
                        resultado.Error = "Error al registrar el usuario";
                        return resultado;
                    }
                }
                else
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Error = "El correo no es valido, utilice expresiones correspondientes";
                    return resultado;
                }
            }
            resultado.Ok = false;
            resultado.CodigoEstado = 400;
            resultado.Error = "Ya existe el correo ingresado";
            return resultado;

        }
        private bool ValidarMail(string email)
        {
            var usuario = context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
            if (usuario!=null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool validarExpresion(string email)
        {
            return email != null && Regex.IsMatch(email, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$");
        }
        
    }
}
