namespace VillaDoMarApi.Entities
{
    public class Financial
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string PaymentType { get; set; } = null!;
        public string MoveType { get; set; } = null!;
    }
}
