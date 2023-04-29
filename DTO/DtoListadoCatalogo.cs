namespace FrancaSW.DTO
{
    public class DtoListadoCatalogo
    {
        public int IdCatalogo { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = null!;
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoProducto { get; set; }
        public string ColorProducto { get; set; }
        public int MedidaProducto { get; set; }
        public decimal PrecioBocha { get; set; }
        public string DisenioProducto { get; set; }
        public string Imagen { get; set; } = null!;
    }
}
