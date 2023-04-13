using FrancaSW.Models;

namespace FrancaSW.Commands.Combos
{
    public class CombosItems
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }

        public static implicit operator CombosItems(ColoresProducto entity)
        {
            return new CombosItems
            {
                Id = entity.IdColorProducto,
                Descripcion = entity.Descripcion
            };
        }

        public static implicit operator CombosItems(DiseniosProducto entity)
        {
            return new CombosItems
            {
                Id = entity.IdDisenio,
                Descripcion = entity.Descripcion
            };
        }

        public static implicit operator CombosItems(TiposProducto entity)
        {
            return new CombosItems
            {
                Id = entity.IdTipoProducto,
                Descripcion = entity.Descripcion
            };
        }

        public static implicit operator CombosItems(PreciosBocha entity)
        {
            return new CombosItems
            {
                Id = entity.IdPreciosBocha,
                Descripcion = entity.Precio.ToString()
            };
        }

        public static implicit operator CombosItems(MedidasProducto entity)
        {
            return new CombosItems
            {
                Id = entity.IdMedidaProducto,
                Descripcion = entity.Descripcion.ToString()
            };
        }
    }
}
