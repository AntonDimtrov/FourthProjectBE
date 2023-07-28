using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.BookingModels;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.Infrastructure
{
    public class BookingService : IBookingService
    {
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IUserService _userService;

        public BookingService(ElectricVehiclesContext EVContext, IElectricVehicleService vehicleService, IUserService userService)
        {
            _EVContext = EVContext;
            _vehicleService = vehicleService;
            _userService = userService;
        }

        public bool ExisitingBookingsInDB()
        {
            return _EVContext.Bookings.Any();
        }

        public void AddBookingToDB(Booking booking)
        {
            _EVContext.Bookings.Add(booking);
            _EVContext.SaveChanges();
        }

        public void RemoveBookingFromDB(Booking booking)
        {
            _EVContext.Bookings.Remove(booking);
            _EVContext.SaveChanges();
        }

        public bool CheckIfBookingExists(int bookingId)
        {
            return _EVContext.Bookings.Where(b => b.Id == bookingId).Any();
        }

        public ICollection<ElectricVehicle> GetAvailableVehicles()
        {
            List<int> bookedVehicleIds = _EVContext.Bookings.Select(b => b.ElectricVehicleId).ToList();
            return _EVContext.ElectricVehicles.Where(ev => !bookedVehicleIds.Contains(ev.Id)).ToList();
        }

        public ICollection<ElectricVehicle> GetBookedVehicles()
        {
            ICollection<ElectricVehicle> bookedVehicles = new List<ElectricVehicle>();

            foreach (var booking in _EVContext.Bookings)
            {
                bookedVehicles.Add(_vehicleService.GetVehicleByID(booking.ElectricVehicleId));
            }
            return bookedVehicles;
        }

        public Booking GetBookingByCarID(int carId)
        {
            return _EVContext.Bookings.Where(b => b.ElectricVehicleId == carId).FirstOrDefault();
        }

        public Booking GetBookingByID(int bookingId)
        {
            return _EVContext.Bookings.Where(b => b.Id == bookingId).FirstOrDefault();
        }

        public ICollection<Booking> GetBookings()
        {
            return _EVContext.Bookings.ToList();
        }

        public ICollection<Booking> GetUserBookings(int userId)
        {
            var userBookings = _EVContext.Bookings
                .Where(b => b.UserId == userId);

            return userBookings.ToList();
        }

        public ICollection<int> GetUserBookedVehicleIds(int userId)
        {
            List<int> userBookedVehicleIds = GetUserBookings(userId)
                .Select(b => b.ElectricVehicleId)
                .ToList();

            return userBookedVehicleIds;
        }

        public int CreateBooking(int userId, int vehicleId, DateTime startDate, DateTime endDate)
        {
            Booking newBooking = new()
            {
                UserId = userId,
                ElectricVehicleId = vehicleId,
                StartDate = startDate,
                EndDate = endDate,
                User = _userService.GetUserByID(userId),
                ElectricVehicle = _vehicleService.GetVehicleByID(vehicleId)
            };

            AddBookingToDB(newBooking);

            return newBooking.Id;
        }

        public int GetBookingUserId(int bookingId)
        {
            return _EVContext.Bookings
                .FirstOrDefault(b => b.Id == bookingId)
                .UserId;
        }

        public bool VehicleIsBooked(int vehicleId)
        {
            return _EVContext.Bookings.Where(b => b.ElectricVehicleId == vehicleId).Any();
        }

        public int GetBookingVehicleId(int bookingId)
        {
            return _EVContext.Bookings.FirstOrDefault(b => b.Id == bookingId).ElectricVehicleId;
        }

        public ICollection<BookingResponseModel> CreateBookingResponseModels(List<int> bookingIds)
        {
            ICollection<BookingResponseModel> models = new List<BookingResponseModel>();

            foreach (var bookingId in bookingIds)
            {
                Booking booking = GetBookingByID(bookingId);

                BookingResponseModel newModel = new()
                {
                    UserId = booking.UserId,
                    VehicleId = booking.ElectricVehicleId
                };

                models.Add(newModel);
            }

            return models;
        }
    }
}
