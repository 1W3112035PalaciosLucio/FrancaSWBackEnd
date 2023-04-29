﻿namespace FrancaSW.DTO
{
    public class DtoAgregarCatalogo
    {
        public int IdCatalogo { get; set; }

        public int IdProducto { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Imagen { get; set; } = null!;



        public int Codigo { get; set; }

        public int IdTipoProducto { get; set; }

        public int IdColorProducto { get; set; }

        public int IdMedidaProducto { get; set; }

        public int IdPreciosBocha { get; set; }

        public int IdDisenioProducto { get; set; }

        public string Nombre { get; set; }

        public bool? Activo { get; set; }
    }
}
