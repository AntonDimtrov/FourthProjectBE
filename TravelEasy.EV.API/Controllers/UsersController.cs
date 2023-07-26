using Microsoft.AspNetCore.Mvc;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.UserModels;
using TravelEasy.EV.Infrastructure;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly IBookingService _bookingService;
        public UsersController(IUserService userService,
            IElectricVehicleService vehicleService, IBookingService bookingService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var user = _userService.GetUserByID(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetAll()
        {
            bool databaseHasUsers = _userService.ExistingUsersInDB();
            return databaseHasUsers ? Ok(_userService.GetUsers()) : NotFound("No users in DB");
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserLoginRequestModel model)
        {
            User? user = _userService.GetUserByUsername(model.Username);

            if (user == null)
            {
                return BadRequest();
            }

            if (user.Password != model.Password)
            {
                return BadRequest();
            }

            return Ok(user.Id);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserRegisterRequestModel model)
        {
            User? existingUser = _userService.GetUserByUsername(model.Username);

            if (existingUser != null)
            {
                return BadRequest();
            }

            User user = new()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
            };

            _userService.AddUser(user);

            return Created(nameof(UsersController), user.Id);
        }

        [HttpDelete("{id}")]
        public void Remove(int userId)
        {
            _userService.RemoveUser(_userService.GetUserByID(userId));
        }
    }
}