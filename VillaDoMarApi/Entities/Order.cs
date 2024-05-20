namespace VillaDoMarApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = null!;
        public decimal TotalValue { get; set; }
        public Client Client { get; set; } = null!;
    }
}
