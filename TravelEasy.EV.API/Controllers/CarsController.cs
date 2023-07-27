using Microsoft.AspNetCore.Mvc;
using TravelEasy.ElectricVehicles.DB.Models;

using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public CarsController(IUserService userService,
            IElectricVehicleService vehicleService, IBookingService bookingService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
        }

        // Get all vehicles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> Get([System.Web.Http.FromUri] int userId)
        {
            if (!_userService.CheckIfUserExists(userId))
            {
                return Unauthorized();
            }

            var vehicles = _vehicleService.GetVehicles();

            if (!vehicles.Any())
            {
                return Ok("No EVs in database");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var vehicle in vehicles)
            {
                AllEVResponseModel newModel = new()
                {
                    BrandId = vehicle.BrandId,
                    Model = vehicle.Model,
                    PricePerDay = vehicle.PricePerDay,
                    ImageURL = vehicle.ImageURL
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
            if (!_userService.CheckIfUserExists(userId))
            {
                return Unauthorized();
            }

            var bookedVehicles = _bookingService.GetAvailableVehicles();
            if (!bookedVehicles.Any())
            {
                return Ok("No free EVs in database");
            }

            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var vehicle in bookedVehicles)
            {
                AllEVResponseModel newModel = new()
                {
                    BrandId = vehicle.BrandId,
                    Model = vehicle.Model,
                    PricePerDay = vehicle.PricePerDay,
                    ImageURL = vehicle.ImageURL
                };

                models.Add(newModel);
            }

            return Ok(models);
        }



        // Get by ID
        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<EVResponseModel> Get(int id, [System.Web.Http.FromUri] int userId)
        {

            if (!_userService.CheckIfUserExists(userId))
            {
                return Unauthorized();
            }

            ElectricVehicle? ev = _vehicleService.GetVehicleByID(id);

            if (ev == null)
            {
                return NotFound();
            }

            EVResponseModel result = new()
            {

                BrandId = ev.BrandId,
                Model = ev.Model,
                HorsePower = ev.HorsePowers,
                Range = ev.Range,
                PricePerDay = ev.PricePerDay,
                ImageURL = ev.ImageURL,
                CategoryId = ev.CategoryId,
            };

            return Ok(result);
        }
    }
}

