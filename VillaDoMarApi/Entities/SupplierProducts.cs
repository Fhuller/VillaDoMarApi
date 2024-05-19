namespace VillaDoMarApi.Entities
{
    public class SupplierProducts
    {
        public int Id { get; set; }
        public Supplier Supplier { get; set; }
        public RawProduct RawProduct { get; set; }
        public Waste Waste { get; set; }
    }
}
