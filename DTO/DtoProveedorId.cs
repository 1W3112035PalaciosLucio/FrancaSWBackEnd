namespace FrancaSW.DTO
{
    public class DtoProveedorId
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public decimal Telefono { get; set; }
        public int IdLocalidad { get; set; }
        public int IdProvincia { get; set; }
    }
}
