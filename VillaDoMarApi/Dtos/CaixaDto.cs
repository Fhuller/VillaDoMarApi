namespace VillaDoMarApi.Dtos
{
    public class CaixaDto
    {
        public string Name { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string TipoPagamento { get; set; } = null!;
        public string TipoMovimento { get; set; } = null!;
    }

    public class CaixaIdDto : CaixaDto
    {
        public int Id { get; set; }
    }
}
