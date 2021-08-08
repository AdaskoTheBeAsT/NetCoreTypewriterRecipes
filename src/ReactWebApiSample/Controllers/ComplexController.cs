using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReactWebApiSample.Attributes;
using ReactWebApiSample.Models;

namespace ReactWebApiSample.Controllers
{
    [GenerateFrontendType]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
#pragma warning disable SEC0019 // Missing AntiForgeryToken Attribute
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Post([FromBody]CombinedResultModel value)
#pragma warning restore SEC0120 // Missing Authorization Attribute
#pragma warning restore SEC0019 // Missing AntiForgeryToken Attribute
        {
            return Ok(value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
#pragma warning disable SEC0120 // Missing Authorization Attribute
        public IActionResult Get(CombinedQueryModel query)
#pragma warning restore SEC0120 // Missing Authorization Attribute
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            return Ok(new CombinedResultModel
            {
                Id = query.Id,
                SampleList = new List<ComplexBaseModel>
                {
                    new ComplexAModel
                    {
                        Id = 2,
                        Text = "OK",
                    },
                    new ComplexBModel
                    {
                        Id = 3,
                        IsActive = true,
                    },
                },
            });
        }
    }
}
