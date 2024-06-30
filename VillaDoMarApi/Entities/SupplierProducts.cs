namespace VillaDoMarApi.Entities
{
    public class SupplierProducts
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public RawProduct RawProduct { get; set; } = null!;
        public Waste Waste { get; set; } = null!;
    }
}
