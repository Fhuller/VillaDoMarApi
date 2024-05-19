using VillaDoMarApi.Entities;

namespace VillaDoMarApi.Dtos
{
    public class OrderDto
    {
        public List<Product> Products { get; set; } = null!;
        //public Client Client { get; set; } quando tiver cliente descomenta e adiciona no createOrder
    }
}
