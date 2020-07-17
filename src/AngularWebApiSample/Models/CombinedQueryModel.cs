using System.Collections.Generic;
using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models
{
    [GenerateFrontendType]
    public class CombinedQueryModel
    {
        public int Id { get; set; }

        public List<SimpleModel> SimpleModels { get; internal set; } = new List<SimpleModel>();
    }
}
