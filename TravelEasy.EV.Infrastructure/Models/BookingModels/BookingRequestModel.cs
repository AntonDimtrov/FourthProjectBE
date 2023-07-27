namespace TravelEasy.EV.Infrastructure.Models.BookingModels
{
    public class BookingRequestModel
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
