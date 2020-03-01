using System;
using System.Collections.Generic;
using AngularWebApiSample.Attributes;
using AngularWebApiSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiSample.Controllers
{
    [GenerateFrontendType]
    public class ComplexController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
        public IActionResult Post([FromBody]CombinedResultModel value)
        {
            return Ok(value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
        public IActionResult Get(CombinedQueryModel query)
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
