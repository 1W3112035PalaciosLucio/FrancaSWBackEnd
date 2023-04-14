namespace FrancaSW.DTO
{
    public class DtoListaPrMatPrProv
    {
        public int IdPreciosMateriaPrimaProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string NombreMateriaPrima { get; set; }
        public int IdMateriaPrima { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaVigenciaDesde { get; set; }
        public DateTime FechaVigenciaHasta { get; set; }

    }
}
