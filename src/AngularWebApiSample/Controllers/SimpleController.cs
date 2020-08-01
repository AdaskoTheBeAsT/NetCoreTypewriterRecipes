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
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Get()
#pragma warning restore SEC0120 // Missing Authorization Attribute
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Get(int id)
#pragma warning restore SEC0120 // Missing Authorization Attribute
        {
            return Ok("value");
        }

        // POST api/values
        [HttpPost]
#pragma warning disable SEC0019 // Missing AntiForgeryToken Attribute
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Post([FromBody]string value)
#pragma warning restore SEC0120 // Missing Authorization Attribute
#pragma warning restore SEC0019 // Missing AntiForgeryToken Attribute
        {
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
#pragma warning disable SEC0019 // Missing AntiForgeryToken Attribute
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Delete(int id)
#pragma warning restore SEC0120 // Missing Authorization Attribute
#pragma warning restore SEC0019 // Missing AntiForgeryToken Attribute
        {
            return Ok();
        }
    }
#pragma warning restore RCS1163,CA1801 // Unused parameter.
}
