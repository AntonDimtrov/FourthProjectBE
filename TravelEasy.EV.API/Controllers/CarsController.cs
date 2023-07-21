using Microsoft.AspNetCore.Mvc;

namespace TravelEasy.EV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok();
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
