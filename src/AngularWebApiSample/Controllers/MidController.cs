using System;
using AngularWebApiSample.Attributes;
using AngularWebApiSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiSample.Controllers
{
#pragma warning disable RCS1163,CA1801 // Unused parameter.
    [GenerateFrontendType]
    public class MidController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(SimpleModel), 200)]
#pragma warning disable SEC0019 // Missing AntiForgeryToken Attribute
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Post([FromBody]SimpleModel value)
#pragma warning restore SEC0120 // Missing Authorization Attribute
#pragma warning restore SEC0019 // Missing AntiForgeryToken Attribute
        {
            return Ok(value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SimpleModel), 200)]
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Get(Guid id)
#pragma warning restore SEC0120 // Missing Authorization Attribute
        {
            return Ok(new SimpleModel { Num = 2, Text = "ok" });
        }
    }
#pragma warning restore RCS1163,CA1801 // Unused parameter.
}
