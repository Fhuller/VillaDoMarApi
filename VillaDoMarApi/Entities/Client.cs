namespace VillaDoMarApi.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime createdDate { get; set; }
        public Address adress { get; set; }
    }
}
