using System.Collections.Generic;
using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models
{
    [GenerateFrontendType]
    public class CombinedResultModel
    {
        public int Id { get; set; }

        public List<ComplexBaseModel> SampleList { get; internal set; } = new List<ComplexBaseModel>();
    }
}
