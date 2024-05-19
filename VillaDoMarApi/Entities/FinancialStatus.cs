namespace VillaDoMarApi.Entities
{
    public class FinancialStatus
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}