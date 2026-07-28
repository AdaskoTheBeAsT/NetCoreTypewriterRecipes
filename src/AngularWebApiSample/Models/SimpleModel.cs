using AdaskoTheBeAsT.Typewriter.Annotations;

namespace AngularWebApiSample.Models;

[GenerateFrontendType]
public record SimpleModel(
    int Num,
    string? Text,
    bool IsOk,
    FirstSet FirstSet,
    SecondSet SecondSet);
