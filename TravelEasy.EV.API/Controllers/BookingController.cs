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
            if (!_userService.CheckIfUserExists(userId))
            {
                return Unauthorized("User does not exist");
            }

            List<int> userBookedVehiclesIds = _bookingService.GetUserBookedVehicles(userId).Select(ev => ev.Id).ToList();

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
            int? bookingId = _bookingService.GetBookingByCarID(carId).Id;

            if (bookingId == null)
            {
                return BadRequest("Car is not booked");
            }

            return Ok(bookingId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> Post([FromBody] BookingRequestModel request)
        {
            if (!_userService.CheckIfUserExists(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            if (!_vehicleService.CheckIfVehicleExists(request.VehicleId))
            {
                return BadRequest("Car does not exist");
            }

            if (_vehicleService.VehicleIsBooked(request.VehicleId))
            {
                return Ok("Car is already booked");
            }

            int newBookingId = _bookingService.CreateBooking(request.UserId, request.VehicleId, request.StartDate, request.EndDate);

            return Ok(newBookingId);
        }

        [HttpPut("cancel-booking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BookingResponseModel> Put([FromBody] BookingRequestModel request)
        {

            if (!_userService.CheckIfUserExists(request.UserId))
            {
                return Unauthorized("User does not exist");
            }

            if (!_vehicleService.CheckIfVehicleExists(request.VehicleId))
            {
                return NotFound($"Car with id {request.VehicleId} doesn't exist");
            }

            int? bookingId = _bookingService.GetBookingByCarID(request.VehicleId).Id;

            if (bookingId == null)
            {
                return BadRequest("Car is not booked");
            }

            if (_bookingService.GetBookingUserId((int)bookingId) != request.UserId)
            {
                return BadRequest("Car is booked by another user");
            }

            _bookingService.RemoveBooking(_bookingService.GetBookingByID((int)bookingId));

            return Ok("Booking cancelled successfully");
        }
    }
}
