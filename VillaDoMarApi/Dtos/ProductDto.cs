namespace VillaDoMarApi.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Value { get; set; }
        public decimal Weight { get; set; }
        public int TypeProductID { get; set; }
    }

    public class ProductIdDto : ProductDto
    {
        public int Id { get; set; }
    }

    public class ProductMovementDto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public bool IsEntry { get; set; }
        public string? Description { get; set; }
    }
}
