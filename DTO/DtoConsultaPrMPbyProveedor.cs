namespace FrancaSW.DTO
{
    public class DtoConsultaPrMPbyProveedor
    {
        public int idProveedor { get; set; }
        public int idMateriaPrima { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Codigo { get; set; }
        public string MateriaPrima { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public decimal Precio { get; set; }
    }
}
