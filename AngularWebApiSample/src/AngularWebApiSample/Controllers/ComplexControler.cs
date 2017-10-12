using Microsoft.AspNetCore.Mvc;

namespace AngularWebApiSample.Controllers
{
    using System.Collections.Generic;
    using Models;

    public class ComplexControler : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
        public IActionResult Post([FromBody]CombinedResultModel value)
        {
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(CombinedResultModel), 200)]
        public IActionResult Get(CombinedQueryModel query)
        {
            return Ok(new CombinedResultModel
            {
                Id = 2,
                SampleList = new List<ComplexBaseModel>
                {
                    new ComplexAModel
                    {
                        Id = 2,
                        Text = "OK"
                    },
                    new ComplexBModel
                    {
                        Id = 3,
                        IsActive = true
                    }
                }
            });
        }
    }
}
