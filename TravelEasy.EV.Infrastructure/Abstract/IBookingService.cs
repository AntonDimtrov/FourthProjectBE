using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Models.BookingModels;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IBookingService
    {
        public bool CheckIfBookingExists(int bookingId);
        public ICollection<Booking> GetUserBookings(int userId);
        public Booking GetBookingByID(int bookingId);
        public Booking GetBookingByCarID(int bookingId);
        public ICollection<ElectricVehicle> GetBookedVehicles();
        public ICollection<ElectricVehicle> GetAvailableVehicles();
        public void AddBookingToDB(Booking booking);
        public void RemoveBookingFromDB(Booking booking);
        public ICollection<Booking> GetBookings();
        public ICollection<int> GetUserBookedVehicleIds(int userId);
        public int GetBookingUserId(int bookingId);
        public int GetBookingVehicleId(int bookingId);
        public int CreateBooking(int userId, int vehicleId, DateTime startDate, DateTime endDate);
        ICollection<BookingResponseModel> CreateBookingResponseModels(List<int> bookingIds);
        bool VehicleIsBooked(int vehicleId);
        bool ExisitingBookingsInDB();
    }
}
