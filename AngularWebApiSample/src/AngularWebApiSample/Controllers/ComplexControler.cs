using System;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiSample.Controllers
{
    using System.Collections.Generic;
    using AngularWebApiSample.Models;

    public class ComplexControler : ControllerBase
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
