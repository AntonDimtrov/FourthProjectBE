using Microsoft.AspNetCore.Mvc;
using TravelEasy.EV.DataLayer;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.API.Models.UserModels;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ElectricVehiclesContext _EVContext;

        public UsersController(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var user = _EVContext.Users.Find(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserLoginRequestModel model)
        {
            User? user = _EVContext.Users.Where(u => u.Username == model.Username).FirstOrDefault();

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
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] UserRegisterRequestModel model)
        {
            User? existingUser = _EVContext.Users.Where(u => u.Username == model.Username).FirstOrDefault();

            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            User user = new();
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;

            _EVContext.Users.Add(user);
            _EVContext.SaveChanges();

            return Created(nameof(UsersController), user.Id);
        }
    }
}