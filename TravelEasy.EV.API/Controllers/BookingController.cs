using Microsoft.AspNetCore.Mvc;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.BookingModels;
using TravelEasy.EV.API.Models.EVModels;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public BookingController(IUserService userService,
            IElectricVehicleService vehicleService, IBookingService bookingService)
        {
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
                return NotFound("User has no booked cars");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var booking in userBookings)
            {
                ElectricVehicle? car = _vehicleService.GetVehicleByID(booking.CarId);

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
            Booking? booking = _bookingService.GetBookingByCarID(carId);

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
            if (_vehicleService.VehicleIsBooked(request.CarId))
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

            _bookingService.AddBooking(newBooking);

            ///?
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
            if (!_vehicleService.VehicleExists(request.CarId))
            {
                return NotFound($"Car with id {request.CarId} doesn't exist");
            }

            Booking? booking = _bookingService.GetBookingByCarID(request.CarId);

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

            _bookingService.RemoveBooking(booking);

            return Ok("Booking cancelled successfully");
        }
    }
}
