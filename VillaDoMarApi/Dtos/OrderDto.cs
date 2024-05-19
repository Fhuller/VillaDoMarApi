using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Dtos
{
    public class OrderDto
    {
        public List<Product> Products { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }

    public class OrderIdDto : OrderDto
    {
        public int Id { get; set; }
    }
}
