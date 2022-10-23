using System;
using Microsoft.AspNetCore.Mvc;
using ReactWebApiSample2.Attributes;
using ReactWebApiSample2.Models;

namespace ReactWebApiSample2.Controllers;
#pragma warning disable RCS1163,CA1801 // Unused parameter.
[GenerateFrontendType]
[Route("api/[controller]")]
[ApiController]
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
#pragma warning disable CC0057 // Unused parameters
    public IActionResult Get(Guid id)
#pragma warning restore CC0057 // Unused parameters
#pragma warning restore SEC0120 // Missing Authorization Attribute
    {
        return Ok(new SimpleModel(2, "ok", true, FirstSet.ValA, SecondSet.ValB));
    }
}
#pragma warning restore RCS1163,CA1801 // Unused parameter.
