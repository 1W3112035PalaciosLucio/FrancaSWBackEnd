namespace FrancaSW.DTO
{
    public class DtoPrecioMatPrimaProv
    {
        public int IdPreciosMateriaPrimaProveedor { get; set; }

        public int IdProveedor { get; set; }

        public int IdMateriaPrima { get; set; }

        public DateTime FechaVigenciaDesde { get; set; }

        public DateTime FechaVigenciaHasta { get; set; }

        public decimal Precio { get; set; }
    }
}
