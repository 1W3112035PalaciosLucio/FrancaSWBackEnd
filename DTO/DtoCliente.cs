﻿namespace FrancaSW.DTO
{
    public class DtoCliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public decimal Telefono { get; set; }
        public string Direccion { get; set; } = null!;
        public int IdLocalidad { get; set; }
    }
}
