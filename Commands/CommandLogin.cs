using FrancaSW.Models;
using System.ComponentModel.DataAnnotations;

namespace FrancaSW.Commands
{
    public class CommandLogin
    {

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
        public bool Activo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string[] Roles { get; set; }
        public string Token { get; set; }

        public string Message { get; set; }
        public bool Ok { get; set; }
        public string Error { get; set; }
        public int StateCode { get; set; }

        public static implicit operator CommandLogin(Usuario user)
        {
            if (user == null)
            {
                return null;
            }

            var result =  new CommandLogin
            {
                IdUsuario = user.IdUsuario,
                Activo = user.Activo,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email,
                Roles = user.RolesUsuarios.Select(s => s.IdRolNavigation.Codigo).ToArray()
                
            };

            return result;

        }

    }
}
