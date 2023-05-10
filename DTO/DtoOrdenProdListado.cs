namespace FrancaSW.DTO
{
    public class DtoOrdenProdListado
    {
        public int IdOrdenProduccion { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string NombreProd { get; set; }
        public string NombreUsuario { get; set; }
        public string EstadoOrden { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int NumeroOrden { get; set; }
        public int? Cantidad { get; set; }
    }
}
