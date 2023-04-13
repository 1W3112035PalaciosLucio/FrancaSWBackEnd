namespace FrancaSW.Comun
{
    public static class Constantes
    {
        public static class DefaultMessages
        {
            public const string DefaultErrorMessage = "¡Houston, tenemos un problema!. " +
                                                      "Si el problema persiste contacte al equipo de IT.";
            public const string DefaultSuccesMessage = "Los cambios se guardorn exitosamente.";
        }

        public static class DefaultSecurityValues
        {
            public const string DefaultUserName = "usuario1";
        }

        public static class DefinicionRoles
        {
            public const string AccesoTotal = "Tesorero,Contador,Admin,Vendedor";
            public const string Pagos = "Tesorero,Contador,Admin";
            public const string Ventas = "Admin,Vendedor";
        }
    }
}
