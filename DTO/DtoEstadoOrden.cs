namespace FrancaSW.DTO
{
    public class DtoEstadoOrden
    {
        public int NumeroOrden { get; set; }
        public int IdEstadoOrdenProduccion { get; set; }

        public DtoEstadoOrden(int NumeroOrden, int IdEstadoOrdenProduccion)
        {
            this.NumeroOrden = NumeroOrden;
            this.IdEstadoOrdenProduccion = IdEstadoOrdenProduccion;
        }
        public DtoEstadoOrden()
        {
        }
    }
}
