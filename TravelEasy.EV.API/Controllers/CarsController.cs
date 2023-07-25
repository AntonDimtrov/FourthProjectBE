using Microsoft.AspNetCore.Mvc;
using TravelEasy.EV.DataLayer;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.EVModels;
using Azure.Core;
using TravelEasy.EV.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public CarsController(ElectricVehiclesContext EVcontext, IUserService userService,
            IElectricVehicleService vehicleService, IBookingService bookingService)
        {
            _EVContext = EVcontext;
            _userService = userService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
        }

        // Get all electric vehicles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> Get([System.Web.Http.FromUri] int userId)
        {
            // Check if user exists
            if (!_EVContext.Users.Where(u => u.Id == userId).Any())
            {
                return Unauthorized();
            }

            var vehicles = _EVContext.ElectricVehicles;

            if (!vehicles.Any())
            {
                return Ok("No EVs in database");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var vehicle in vehicles)
            {
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

        // Get all available electric vehicles
        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> GetAvailable([System.Web.Http.FromUri] int userId)
        {
            // Check if user does not exist
            if (!_userService.UserExists(userId))
            {
                return Unauthorized("User does not exist");
            }

            var bookedVehicles = _bookingService.GetBookedVehicles();
            if (!bookedVehicles.Any())
            {
                return Ok("No free EVs in database");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var vehicle in bookedVehicles)
            {
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



        // Get by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<EVResponseModel> Get(int id, [System.Web.Http.FromUri] int userId)
        {
            // Check if user does not exist
            if (!_userService.UserExists(userId))
            {
                return Unauthorized("User does not exist");
            }

            ElectricVehicle? ev = _EVContext.ElectricVehicles.Where(ev => ev.Id == id).FirstOrDefault();

            if (ev == null)
            {
                return NotFound();
            }

            EVResponseModel result = new()
            {
                Brand = ev.Brand,
                Model = ev.Model,
                HorsePower = ev.HorsePowers,
                Range = ev.Range,
                PricePerDay = ev.PricePerDay
            };

            return Ok(result);
        }
    }
}
