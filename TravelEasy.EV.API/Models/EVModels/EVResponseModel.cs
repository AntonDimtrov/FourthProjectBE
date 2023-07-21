namespace TravelEasy.EV.API.Models.EVModels
{
    public class EVResponseModel
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? HorsePower { get; set; }
        public int? Range { get; set; }
        public decimal? PricePerDay { get; set; }
    }
}
