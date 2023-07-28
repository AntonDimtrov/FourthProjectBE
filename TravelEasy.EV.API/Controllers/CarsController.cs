using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> Get([System.Web.Http.FromUri] int userId)
        {
            if (!_userService.CheckIfUserExistsById(userId))
            {
                return Unauthorized();
            }

            if (!_vehicleService.ExistingVehiclesInDB())
            {
                return NotFound();
            }

            ICollection<AllEVResponseModel> models = _vehicleService
                .CreateAllEVResponseModels(_vehicleService.GetVehicles().Select(v=>v.Id).ToList());

            return Ok(models);
        }

        [HttpGet("available")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ICollection<AllEVResponseModel>> GetAvailable([System.Web.Http.FromUri] int userId)
        {
            if (!_userService.CheckIfUserExistsById(userId))
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

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<EVResponseModel> Get(int id, [System.Web.Http.FromUri] int userId)
        {

            if (!_userService.CheckIfUserExistsById(userId))
            {
                return Unauthorized();
            }

            if (!_vehicleService.CheckIfVehicleExists(id))
            {
                return BadRequest();
            }

            return Ok(_vehicleService.CreateEVResponseModel(_vehicleService.GetVehicleByID(id)));
        }
    }
}

