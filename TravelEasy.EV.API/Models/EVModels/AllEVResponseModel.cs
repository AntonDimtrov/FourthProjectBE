namespace TravelEasy.EV.API.Models.EVModels
{
    public class AllEVResponseModel
    {
        public int BrandId { get; set; }
        public string? Model { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? ImageURL { get; set; }
    }
}
