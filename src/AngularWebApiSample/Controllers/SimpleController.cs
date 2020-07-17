using AngularWebApiSample.Attributes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace AngularWebApiSample.Controllers
{
#pragma warning disable RCS1163,CA1801 // Unused parameter.
    [GenerateFrontendType]
    [Route("api/[controller]")]
    public class SimpleController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        [ProducesResponseType(typeof(string[]), 200)]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
#pragma warning restore RCS1163,CA1801 // Unused parameter.
}
