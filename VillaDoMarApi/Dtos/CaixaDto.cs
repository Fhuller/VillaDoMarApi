namespace VillaDoMarApi.Dtos
{
    public class CaixaDto
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string PaymentType { get; set; } = null!;
        public string MoveType { get; set; } = null!;
    }

    public class CaixaIdDto : CaixaDto
    {
        public int Id { get; set; }
    }
}
