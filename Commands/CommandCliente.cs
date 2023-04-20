namespace FrancaSW.Commands
{
    public class CommandCliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Telefono { get; set; }
        public string Direccion { get; set; } = null!;
        public int IdLocalidad { get; set; }
    }
}
