namespace FrancaSW.Commands.CommandProductos
{
    public class CommandPrecioProd
    {
        public int IdPreciosBocha { get; set; }
        public DateTime FechaVigenciaDesde { get; set; }
        public DateTime FehcaVigenciaHasta { get; set; }
        public decimal Precio { get; set; }
    }
}
