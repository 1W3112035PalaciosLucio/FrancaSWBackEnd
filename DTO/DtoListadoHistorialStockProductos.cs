namespace FrancaSW.DTO
{
    public class DtoListadoHistorialStockProductos
    {
        public int? IdHistorialProd { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public int IdProducto { get; set; }
        public string? TipoMovimiento { get; set; }
        public string Nombre { get; set; }
    }
}
