namespace Zamazimah.Models.HousingContracts
{
	public class CreateHousingContractModel
	{
		public string Code { get; set; }
		public string HousingName { get; set; }
		public string Location { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int PilgrimsNumber { get; set; }
		public int LocationNatureId { get; set; }
		public string? Latitude { get; set; }
		public string? Longitude { get; set; }
		public string? ResponsableId { get; set; }
		public string? ResidencePermitNumber { get; set; }
		public int? CityId { get; set; }
		public string? WassilNumber { get; set; }
		public int? CenterId { get; set; }
		public string? CommercialHousingName { get; set; }
	}
}
