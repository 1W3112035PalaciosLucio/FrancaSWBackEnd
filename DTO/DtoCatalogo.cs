namespace FrancaSW.DTO
{
    public class DtoCatalogo
    {
        public int IdCatalogo { get; set; }

        public int IdProducto { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Imagen { get; set; } = null!;
    }
}
