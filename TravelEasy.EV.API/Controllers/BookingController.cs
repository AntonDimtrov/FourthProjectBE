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

        [HttpGet("user-booked-cars")]
        public ActionResult<ICollection<BookingResponseModel>> GetBookedCars(int userId)
        {
            // Check if user exists
            if (!_EVContext.Users.Where(u => u.Id == userId).Any())
            {
                return Unauthorized();
            }

            var bookings = _EVContext.Bookings.Where(b => b.UserId == userId);

            if (bookings.IsNullOrEmpty())
            {
                return Ok("User has no booked cars");
            }

            ICollection<BookingResponseModel> models = new List<BookingResponseModel>();

            foreach (var booking in bookings)
            {
                BookingResponseModel model = new()
                {
                    CarId = booking.CarId
                };

                models.Add(model);
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<BookingResponseModel> Post(int userId, int carId)
        {
            // Check if user exists
            if (!_EVContext.Users.Where(u => u.Id == userId).Any())
            {
                return Unauthorized("User does not exist");
            }

            ElectricVehicle? vehicle = _EVContext.ElectricVehicles.Where(ev => ev.Id == carId).FirstOrDefault();

            // Check if car exists
            if (vehicle == null)
            {
                return Unauthorized("Car does not exist");
            }

            // Check if car is available
            if (vehicle.IsBooked)
            {
                return BadRequest("Car is already booked");
            }

            vehicle.IsBooked = true;

            BookingResponseModel model = new()
            {
                CarId = carId
            };

            Booking newBooking = new()
            {
                CarId = carId,
                UserId = userId,
            };

            /*_EVContext.Bookings.Add(new Booking(CarId = carId, UserId = userId));
            _EVContext.SaveChanges();*/

            return Ok(model);
        }

        [HttpPut("makeCarAvailable")]
        public ActionResult<BookingResponseModel> Put([FromBody] int userId, int carId)
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
