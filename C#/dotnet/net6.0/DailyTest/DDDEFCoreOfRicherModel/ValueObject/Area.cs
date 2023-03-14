using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.ValueObject;

[Owned]
public record Area(double Value, AreaType Unit);

public enum AreaType
{
    /// <summary>
    /// 平方米
    /// </summary>
    SquareKM,
    /// <summary>
    /// 公顷
    /// </summary>
    Hectare,
    /// <summary>
    /// 亩
    /// </summary>
    CnMu
}