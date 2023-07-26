using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.Infrastructure
{
    public interface IBookingService
    {
        public bool BookingExists(int bookingId);
        public ICollection<Booking> GetUserBookings(int userId);
        public Booking GetBookingByID(int bookingId);
        public Booking GetBookingByCarID(int bookingId);
        public ICollection<ElectricVehicle> GetBookedVehicles();
        public void AddBooking(Booking booking);
        public void RemoveBooking(Booking booking);
    }
}
