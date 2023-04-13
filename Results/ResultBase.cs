namespace FrancaSW.Results
{
    public class ResultBase
    {
        public string Message { set; get; } = null!;
        public bool Ok { set; get; }
        public string Error { get; set; }
        public int CodigoEstado { set; get; }
        public dynamic Resultado { get; set; }
    }
}
