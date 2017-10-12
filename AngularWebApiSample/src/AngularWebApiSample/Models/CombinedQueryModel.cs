namespace AngularWebApiSample.Models
{
    using System.Collections.Generic;

    public class CombinedQueryModel
    {
        public int Id { get; set; }

        public List<SimpleModel> SimpleModels { get; set; }
    }
}
