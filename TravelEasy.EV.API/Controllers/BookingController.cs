using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.BookingModels;
using TravelEasy.EV.API.Models.EVModels;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        //remove this
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public BookingController(ElectricVehiclesContext EVcontext, IUserService userService,
            IElectricVehicleService vehicleService, IBookingService bookingService)
        {
            _EVContext = EVcontext;
            _userService = userService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
        }

        // Get user booked cars
        [HttpGet("user-booked-cars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> GetBookedCars(int userId)
        {
            // Check if user does not exist
            if (!_userService.UserExists(userId))
            {
                return Unauthorized("User does not exist");
            }


            var userBookings = _bookingService.GetUserBookings(userId);

            //Check if user has any booked cars
            if (!userBookings.Any())
            {
                //?
                return Ok("User has no booked cars");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var booking in userBookings)
            {
                ElectricVehicle? car = _EVContext.ElectricVehicles.Where(ev => ev.Id == booking.CarId).FirstOrDefault();

                AllEVResponseModel newModel = new()
                {
                    Brand = car.Brand,
                    Model = car.Model,
                    PricePerDay = car.PricePerDay
                };

                models.Add(newModel);
            }
            return Ok(models);
        }

        // Get booking by car Id
        [HttpGet("{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Booking> Get(int carId)
        {
            Booking? booking = _EVContext.Bookings.Where(b => b.CarId == carId).FirstOrDefault();

            // Check if booking exists
            if (booking == null)
            {
                return BadRequest("Car is not booked");
            }

            return Ok(booking);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BookingResponseModel> Post([FromBody] BookingRequestModel request)
        {
            // Check if user does not exist
            if (!_userService.UserExists(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            // Check if car does not exist
            if (!_vehicleService.VehicleExists(request.CarId)) 
            {
                return BadRequest("Car does not exist");
            }

            // Check if car is available
            if (_EVContext.Bookings.Where(b => b.CarId == request.CarId).Any())
            {
                return Ok("Car is already booked");
            }

            Booking newBooking = new()
            {
                CarId = request.CarId,
                UserId = request.UserId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            _EVContext.Bookings.Add(newBooking);
            _EVContext.SaveChanges();

            BookingResponseModel model = new()
            {
                SuccsessfullyBooked = true
            };

            return Ok(model);
        }

        [HttpPut("cancel-booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BookingResponseModel> Put([FromBody] BookingRequestModel request)
        {

            // Check if user does not exist
            if (!_userService.UserExists(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            // Check if car exists
            if (!_EVContext.ElectricVehicles.Where(ev => ev.Id == request.CarId).Any())
            {
                return NotFound($"Car with id {request.CarId} doesn't exist");
            }

            Booking? booking = _EVContext.Bookings.Where(ev => ev.Id == request.CarId).FirstOrDefault();

            // Check if car is booked
            if (booking == null)
            {
                return BadRequest("Car is not booked");
            }

            // Check if user has booked given car
            if (booking.UserId != request.UserId)
            {
                return BadRequest("Car is booked by another user");
            }

            _EVContext.Bookings.Remove(booking);
            _EVContext.SaveChanges();

            return Ok("Booking cancelled successfully");
        }
    }
}
