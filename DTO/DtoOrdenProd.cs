namespace FrancaSW.DTO
{
    public class DtoOrdenProd
    {
        public int IdOrdenProduccion { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public int IdEstadoOrdenProduccion { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int NumeroOrden { get; set; }
        public int? Cantidad { get; set; }
    }
}
