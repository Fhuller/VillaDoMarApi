namespace VillaDoMarApi.Dtos
{
	public class StatusCaixaDto
	{
		public int Id { get; set; }
		public bool Aberto { get; set; }
		public DateTime DataHoraAbertura { get; set; }
		public DateTime? DataHoraFechamento { get; set; }
	}
}
