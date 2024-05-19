namespace VillaDoMarApi.Dtos
{
	public class StatusCaixaDto
	{
		public int Id { get; set; }
		public bool Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
