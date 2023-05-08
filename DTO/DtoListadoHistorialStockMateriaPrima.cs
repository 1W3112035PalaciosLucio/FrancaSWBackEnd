namespace FrancaSW.DTO
{
    public class DtoListadoHistorialStockMateriaPrima
    {
        public int IdHistorial { get; set; }
        public int? IdMateriaPrima { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public string? TipoMovimiento { get; set; }
    }
}
