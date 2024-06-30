namespace VillaDoMarApi.Entities
{
    public class RawProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PricePaid { get; set; }
        public int Weight {  get; set; }
    }
}
