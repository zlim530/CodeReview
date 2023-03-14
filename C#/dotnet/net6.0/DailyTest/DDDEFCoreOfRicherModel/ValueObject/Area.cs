using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.ValueObject;

[Owned]
public record Area(double Value, AreaType Unit);

public enum AreaType
{
    /// <summary>
    /// ƽ����
    /// </summary>
    SquareKM,
    /// <summary>
    /// ����
    /// </summary>
    Hectare,
    /// <summary>
    /// Ķ
    /// </summary>
    CnMu
}