namespace FrancaSW.DTO
{
    public class DtoListaPrecioMatPrProv
    {
        public int IdPreciosMateriaPrimaProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public bool? Activo { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; } = null!;
        public string NombreMateriaPrima { get; set; }
        public int IdMateriaPrima { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaVigenciaDesde { get; set; }
        public DateTime FechaVigenciaHasta { get; set; }
    }
}
