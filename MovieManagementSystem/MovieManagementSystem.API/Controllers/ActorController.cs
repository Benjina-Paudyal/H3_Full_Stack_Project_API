using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {

        // SAMPLE CODE FOR STARTING POINT
        // GET: api/<ActorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ActorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
