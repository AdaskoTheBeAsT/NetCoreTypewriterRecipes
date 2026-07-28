using AdaskoTheBeAsT.Typewriter.Annotations;

namespace ReactWebApiSample.Models;

[GenerateFrontendType]
public record SimpleModel(
    int Num,
    string? Text,
    bool IsOk,
    FirstSet FirstSet,
    SecondSet SecondSet);
