namespace TravelEasy.EV.Infrastructure.Models.EVModels
{
    public class EVResponseModel
    {
        public int BrandId { get; set; }
        public string? Model { get; set; }
        public int? HorsePower { get; set; }
        public int? Range { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? ImageURL { get; set; }
        public int CategoryId { get;set; }
    }
}
