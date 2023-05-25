namespace FrancaSW.DTO
{
    public class DtoListadoStockProductos
    {
        public int IdStockProducto { get; set; }
        public int Codigo { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
    }
}
