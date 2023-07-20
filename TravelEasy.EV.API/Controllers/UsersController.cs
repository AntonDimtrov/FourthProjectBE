using Microsoft.AspNetCore.Mvc;
using TravelEasy.EV.API.Models;
using TravelEasy.EV.DataLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ElectricVehiclesContext _EVContext;
        
        public UsersController(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var user = _EVContext.Users.Find(id);
            return user == null? NotFound(user) : Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost("Login")]
        
        public IActionResult Post([FromBody] UserLoginRequestModel model)
        {
            var response = new UserLoginResponseModel();
            response.Id = 1;
            return Ok(response);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserLoginRequestModel model)
        {
            

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
