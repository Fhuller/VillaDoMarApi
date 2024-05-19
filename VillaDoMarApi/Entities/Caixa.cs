namespace VillaDoMarApi.Entities
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Cliente { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string TipoPagamento { get; set; }
        public string TipoMovimento { get; set; }
    }
}
