namespace FrancaSW.DTO
{
    public class DtoListadoCliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Telefono { get; set; }
        public string Direccion { get; set; } = null!;
        public string Localidad { get; set; }
        public string Provincia { get; set; } = null!;
    }
}
