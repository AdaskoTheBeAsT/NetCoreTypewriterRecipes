using Microsoft.AspNetCore.Mvc;
using ReactWebApiSample2.Attributes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ReactWebApiSample2.Controllers;
#pragma warning disable RCS1163,CA1801 // Unused parameter.
[GenerateFrontendType]
[Route("api/[controller]")]
[ApiController]
public class SimpleController : ControllerBase
{
    // GET: api/values
    [HttpGet]
    [ProducesResponseType(typeof(string[]), 200)]
#pragma warning disable SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
    public IActionResult Get()
#pragma warning restore SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
    {
        return Ok(new [] { "value1", "value2" });
    }

    // GET api/values/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), 200)]
#pragma warning disable CC0057 // Unused parameters
#pragma warning disable SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
    public IActionResult Get(int id)
#pragma warning restore SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
#pragma warning restore CC0057 // Unused parameters
    {
        return Ok("value");
    }

    // POST api/values
    [HttpPost]

    // The Action is missing the AntiForgeryToken attribute. If this action modifies data on the backend,
    // it could be vulnerable to Cross-Site Request Forgery attacks.
#pragma warning disable SEC0019
#pragma warning disable SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
#pragma warning disable CC0057 // Unused parameters
    public IActionResult Post([FromBody]string value)
#pragma warning restore CC0057 // Unused parameters
#pragma warning restore SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
#pragma warning restore SEC0019
    {
        return Ok();
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]

    // The Action is missing the AntiForgeryToken attribute. If this action modifies data on the backend,
    // it could be vulnerable to Cross-Site Request Forgery attacks.
#pragma warning disable SEC0019
#pragma warning disable SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
#pragma warning disable SCS0016 // Controller method is potentially vulnerable to Cross Site Request Forgery (CSRF).
#pragma warning disable CC0057 // Unused parameters
    public IActionResult Delete(int id)
#pragma warning restore CC0057 // Unused parameters
#pragma warning restore SCS0016 // Controller method is potentially vulnerable to Cross Site Request Forgery (CSRF).
#pragma warning restore SEC0120 //The Action is missing the Authorization attribute and can be invoked by anonymous users.
#pragma warning restore SEC0019
    {
        return Ok();
    }
}
#pragma warning restore RCS1163,CA1801 // Unused parameter.
