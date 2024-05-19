namespace VillaDoMarApi.Entities
{
    public class Lancamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
