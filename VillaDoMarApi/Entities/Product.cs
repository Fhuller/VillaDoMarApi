namespace VillaDoMarApi.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Weight { get; set; }
        public int TypeProduct {  get; set; }
        public int SupplierProductId { get; set; }
    }
}
