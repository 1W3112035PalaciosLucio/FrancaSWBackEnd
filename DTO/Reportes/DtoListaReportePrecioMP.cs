namespace FrancaSW.DTO.Reportes
{
    public class DtoListaReportePrecioMP
    {
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public string FechaUltimaActualizacion { get; set; }
    }
}
