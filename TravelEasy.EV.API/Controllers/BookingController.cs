using Microsoft.AspNetCore.Mvc;
using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.BookingModels;
using TravelEasy.EV.Infrastructure.Models.EVModels;

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

        [HttpGet("user-booked-cars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> GetBookedCars(int userId)
        {
            if (!_userService.CheckIfUserExistsById(userId))
            {
                return Unauthorized("User does not exist");
            }

            List<int> userBookedVehiclesIds = _bookingService.GetUserBookedVehicleIds(userId).ToList();

            if (!userBookedVehiclesIds.Any())
            {
                return NotFound("User has no booked cars");
            }
            
            return Ok(_vehicleService.CreateAllEVResponseModels(userBookedVehiclesIds));
        }

        [HttpGet("{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> Get(int carId)
        {
            if (!_vehicleService.CheckIfVehicleExists(carId))
            {
                return BadRequest("Car does not exist");
            }

            if (!_bookingService.CheckIfBookingExists(carId))
            {
                return BadRequest("Car is not booked");

            }

            return Ok(_bookingService.GetBookingByCarID(carId).Id);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<BookingResponseModel>> GetAll()
        {
            if (!_bookingService.GetBookings().Any())
            {
                return Ok("No bookings made");
            }

            List<int> bookingIds = _bookingService.GetBookings().Select(b=>b.Id).ToList();

            ICollection<BookingResponseModel> models= _bookingService.CreateBookingResponseModels(bookingIds).ToList();

            return Ok(models);
        }

        [HttpPost]
        [Route("book-vehicle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> Post([FromBody] BookingRequestModel request)
        {
            if (!_userService.CheckIfUserExistsById(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            if (!_vehicleService.CheckIfVehicleExists(request.VehicleId))
            {
                return BadRequest("Car does not exist");
            }

            if (_bookingService.VehicleIsBooked(request.VehicleId))
            {
                return Ok("Car is already booked");
            }

            int newBookingId = _bookingService.CreateBooking(request.UserId, request.VehicleId, request.StartDate, request.EndDate);

            return Ok(newBookingId);
        }

        [HttpDelete("cancel-booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BookingResponseModel> Put([FromBody] BookingRequestModel request)
        {

            if (!_userService.CheckIfUserExistsById(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            if (!_vehicleService.CheckIfVehicleExists(request.VehicleId))
            {
                return NotFound($"Car does not exist");
            }

            if (!_bookingService.VehicleIsBooked(request.VehicleId))
            {
                return BadRequest("Car is not booked");
            }

            int bookingId = _bookingService.GetBookingByCarID(request.VehicleId).Id;

            if (_bookingService.GetBookingUserId(bookingId) != request.UserId)
            {
                return BadRequest("Car is booked by another user");
            }

            _bookingService.RemoveBookingFromDB(_bookingService.GetBookingByID(bookingId));

            return Ok("Booking cancelled successfully");
        }
    }
}
