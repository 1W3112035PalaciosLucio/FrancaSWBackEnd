namespace FrancaSW.DTO
{
    public class DtoListadoProveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public decimal Telefono { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; } = null!;
        public bool? Activo { get; set; }
    }
}
