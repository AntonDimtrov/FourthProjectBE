using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.IdentityModel.Tokens;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.BookingModels;
using TravelEasy.EV.API.Models.EVModels;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ElectricVehiclesContext _EVContext;
        public BookingController(ElectricVehiclesContext EVcontext)
        {
            _EVContext = EVcontext;
        }

        // Get user booked cars
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("user-booked-cars")]
        public ActionResult<ICollection<AllEVResponseModel>> GetBookedCars(int userId)
        {
            // Check if user exists
            if (!_EVContext.Users.Where(u => u.Id == userId).Any())
            {
                return Unauthorized();
            }

            var bookings = _EVContext.Bookings.Where(b => b.UserId == userId);

            //Check if user has any booked cars
            if (bookings.IsNullOrEmpty())
            {
                return Ok("User has no booked cars");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var booking in bookings)
            {
                ElectricVehicle? vehicle = _EVContext.ElectricVehicles.Where(ev => ev.Id == booking.CarId).FirstOrDefault();

                AllEVResponseModel newModel = new()
                {
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    PricePerDay = vehicle.PricePerDay
                };

                models.Add(newModel);
            }
            return Ok(models);
        }

        // Get booking by car Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{carId}")]
        public ActionResult<Booking> Get(int carId)
        {
            // Check if booking exists
            Booking? booking = _EVContext.Bookings.Where(b => b.CarId == carId).FirstOrDefault();

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
            // Check if user exists
            if (!_EVContext.Users.Where(u => u.Id == request.UserId).Any())
            {
                return Unauthorized("User does not exist");
            }

            ElectricVehicle? vehicle = _EVContext.ElectricVehicles.Where(ev => ev.Id == request.CarId).FirstOrDefault();

            // Check if car exists
            if (vehicle == null)
            {
                return BadRequest("Car does not exist");
            }

            // Check if car is available
            if (vehicle.IsBooked)
            {
                //set is booked successful
                return Ok("Car is already booked");
            }

            //remove prop move to other dbset
            vehicle.IsBooked = true;

            BookingResponseModel model = new()
            {
                //other props
                CarId = request.CarId
            };

            Booking newBooking = new()
            {
                // dates
                CarId = request.CarId,
                UserId = request.UserId
            };

            _EVContext.Bookings.Add(newBooking);
            _EVContext.SaveChanges();

            return Ok(model);
        }

        [HttpPut("cancel-booking")]
        public ActionResult<BookingResponseModel> Put(int userId, int carId)
        {
            ElectricVehicle? ev = _EVContext.ElectricVehicles.Where(ev => ev.Id == carId).FirstOrDefault();

            // Check if vehicle with such ID exists
            if (ev == null)
            {
                return NotFound($"Car with id {carId} doesn't exist");
            }

            // Check if the given car is booked
            if (!ev.IsBooked)
            {
                return BadRequest("Car is not booked");
            }

            // Check if the given user has booked the given car
            if (_EVContext.Bookings.Where(b => b.CarId == carId && b.UserId == userId).FirstOrDefault().IsNull())
            {
                return BadRequest("Car is booked by another user");
            }

            ev.IsBooked = false;

            return Ok(carId);
        }
    }
}
