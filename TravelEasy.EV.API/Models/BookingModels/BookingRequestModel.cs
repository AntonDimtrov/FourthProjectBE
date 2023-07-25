namespace TravelEasy.EV.API.Models.BookingModels
{
    public class BookingRequestModel
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
