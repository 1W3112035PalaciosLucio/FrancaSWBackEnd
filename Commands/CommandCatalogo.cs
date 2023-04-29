namespace FrancaSW.Commands
{
    public class CommandCatalogo
    {
        public int IdCatalogo { get; set; }

        public int IdProducto { get; set; }

        public string Descripcion { get; set; } = null!;

        public IFormFile Imagen { get; set; } = null!;
    }
}
