using System.Collections.Generic;

namespace AngularWebApiSample.Models
{
    public class CombinedResultModel
    {
        public int Id { get; set; }

        public List<ComplexBaseModel> SampleList { get; internal set; }
    }
}
