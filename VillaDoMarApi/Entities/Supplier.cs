namespace VillaDoMarApi.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CpfCnpj { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public Address Address { get; set; }
    }
}
