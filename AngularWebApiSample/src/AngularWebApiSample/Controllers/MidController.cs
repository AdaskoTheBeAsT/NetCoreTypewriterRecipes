using System;
using AngularWebApiSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiSample.Controllers
{
#pragma warning disable RCS1163,CA1801 // Unused parameter.
    public class MidController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(SimpleModel), 200)]
        public IActionResult Post([FromBody]SimpleModel value)
        {
            return Ok(value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SimpleModel), 200)]
        public IActionResult Get(Guid id)
        {
            return Ok(new SimpleModel { Number = 2, Text = "ok" });
        }
    }
#pragma warning restore RCS1163,CA1801 // Unused parameter.
}
