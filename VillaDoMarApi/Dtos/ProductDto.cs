namespace VillaDoMarApi.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class ProductIdDto : ProductDto
    {
        public int Id { get; set; }
    }
}
