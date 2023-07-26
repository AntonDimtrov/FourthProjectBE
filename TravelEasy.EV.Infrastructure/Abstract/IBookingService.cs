using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IBookingService
    {
        public bool BookingExists(int bookingId);
        public ICollection<Booking> GetUserBookings(int userId);
        public Booking GetBookingByID(int bookingId);
        public Booking GetBookingByCarID(int bookingId);
        public ICollection<ElectricVehicle> GetBookedVehicles();
        public ICollection<ElectricVehicle> GetAvailableVehicles();
        public void AddBooking(Booking booking);
        public void RemoveBooking(Booking booking);
        public ICollection<Booking> GetBookings();
    }
}
