using System.Collections.Generic;
using AngularWebApiSample.Attributes;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public class MultipleLists
{
    public IList<string> List1 { get; set; } = new List<string>();

    public IList<string>? List2 { get; set; }

    public IList<string?> List3 { get; set; } = new List<string?>();

    public IList<string?>? List4 { get; set; }

    public IList<ComplexAModel> List5 { get; set; } = new List<ComplexAModel>();

    public IList<ComplexAModel>? List6 { get; set; }

    public IList<ComplexAModel?> List7 { get; set; } = new List<ComplexAModel?>();

    public IList<ComplexAModel?>? List8 { get; set; }

    public IList<IList<string>> List9 { get; set; } = new List<IList<string>>();

    public IList<IList<string>>? List10 { get; set; }

    public IList<IList<string>?> List11 { get; set; } = new List<IList<string>?>();

    public IList<IList<string>?>? List12 { get; set; }
}
