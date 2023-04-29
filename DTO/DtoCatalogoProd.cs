using System.Drawing;

namespace FrancaSW.DTO
{
    public class DtoCatalogoProd
    {

        //public int idCatalogo { get; set; }
        //public string descripcion { get; set; }
        //public string imagen { get; set; }
        public int IdProducto { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdColorProducto { get; set; }
        public int IdMedidaProducto { get; set; }
        public decimal IdPreciosBocha { get; set; }
        public int IdDisenioProducto { get; set; }

    }
}
