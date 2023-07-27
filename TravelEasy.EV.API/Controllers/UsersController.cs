using Microsoft.AspNetCore.Mvc;
using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.UserModels;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public UsersController(
            IUserService userService,
            IElectricVehicleService vehicleService,
            IBookingService bookingService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserLoginRequestModel> Get(int id)
        {
            var user = _userService.GetUserByID(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<int>> GetAll()
        {
            bool databaseHasUsers = _userService.ExistingUsersInDB();
            return databaseHasUsers ? Ok(_userService.GetUsers().Select(u => u.Id).ToList()) : NotFound("No users in DB");
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserLoginRequestModel model)
        {
            int? userId = _userService.GetUserByUsername(model.Username).Id;

            if (userId == null)
            {
                return BadRequest();
            }

            if (_userService.GetUserByID((int)userId).Password != model.Password)
            {
                return BadRequest();
            }

            return Ok(userId);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserRegisterRequestModel model)
        {
            int? userId = _userService.GetUserByUsername(model.Username).Id;

            if (userId != null)
            {
                return BadRequest();
            }

            _userService.RegisterUser(model.Username, model.Email, model.Password);

            return Created(nameof(UsersController), userId);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Remove(int id)
        {
            _userService.RemoveUserFromDB(_userService.GetUserByID(id));
        }
    }
}