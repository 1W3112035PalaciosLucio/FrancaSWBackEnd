namespace FrancaSW.DTO.Reportes
{
    public class DtoListadoReportes
    {
        public int idProducto { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoProducto { get; set; }
        public string ColorProducto { get; set; }
        public int MedidaProducto { get; set; }
        public decimal PrecioBocha { get; set; }
        public string DisenioProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
    }
}
