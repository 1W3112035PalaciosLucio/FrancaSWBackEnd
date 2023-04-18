namespace FrancaSW.DTO
{
    public class DtoListadoProveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Telefono { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; } = null!;
    }
}
