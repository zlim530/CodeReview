using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.ValueObject;

[Owned]
public record MultilingualString(string Chinese, string? English);