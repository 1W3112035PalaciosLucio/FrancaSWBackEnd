namespace FrancaSW.Commands
{
    public class CommandStockProductos
    {
        public int IdStockProducto { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaUltimaActualizacion { get; set; }

        public string? TipoMovimiento { get; set; }
    }
}
