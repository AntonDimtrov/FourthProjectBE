using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.Infrastructure
{
    public class BookingService : IBookingService
    {
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IElectricVehicleService _vehicleService;
        public BookingService(ElectricVehiclesContext EVContext, IElectricVehicleService vehicleService)
        {
            _EVContext = EVContext;
            _vehicleService = vehicleService;
        }

        public void AddBooking(Booking booking)
        {
            _EVContext.Bookings.Add(booking);
            _EVContext.SaveChanges();
        }

        public bool BookingExists(int bookingId)
        {
            return _EVContext.Bookings.Where(b => b.Id == bookingId).Any();
        }

        public ICollection<ElectricVehicle> GetAvailableVehicles()
        {
            ICollection<ElectricVehicle> availableVehicles = new List<ElectricVehicle>();
            var vehicles = _vehicleService.GetVehicles();

            foreach(var vehicle in vehicles)
            {
                if (GetBookingByCarID(vehicle.Id) == null)
                {
                    availableVehicles.Add(vehicle);
                }
            }
            return availableVehicles;
        }

        public ICollection<ElectricVehicle> GetBookedVehicles()
        {
            ICollection<ElectricVehicle> bookedVehicles = new List<ElectricVehicle>();

            foreach(var booking in _EVContext.Bookings)
            {
                bookedVehicles.Add(_vehicleService.GetVehicleByID(booking.CarId));
            }
            return bookedVehicles;
        }

        public Booking GetBookingByCarID(int carId)
        {
            return _EVContext.Bookings.Where(b => b.CarId == carId).FirstOrDefault();
        }

        public Booking GetBookingByID(int bookingId)
        {
            return _EVContext.Bookings.Where(b => b.Id == bookingId).FirstOrDefault();
        }

        public ICollection<Booking> GetBookings()
        {
            throw new NotImplementedException();
        }

        public ICollection<Booking> GetUserBookings(int userId)
        {
            var userBookings = _EVContext.Bookings.Where(b => b.UserId == userId);

            return userBookings.ToList();
        }

        public void RemoveBooking(Booking booking)
        {
            _EVContext.Bookings.Remove(booking);
            _EVContext.SaveChanges();
        }
    }
}
