namespace FrancaSW.DTO
{
    public class DtoHistorialStockMateriaPrima
    {
        public int IdHistorial { get; set; }
        public int IdMateriaPrima { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
    }
}
