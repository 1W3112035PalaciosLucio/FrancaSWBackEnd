using System.ComponentModel.DataAnnotations;

namespace FrancaSW.Commands
{
    public class CommandRegister
    {
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; }
        /***/
        [Required(ErrorMessage = "El apellido es requerido.")]
        public string Apellido { get; set; }
        /***/
        [Required(ErrorMessage = "El telefono es requerido.")]
        public int Telefono { get; set; }
        /***/
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }
        /***/
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contrasenia { get; set; }

        
    }
}
