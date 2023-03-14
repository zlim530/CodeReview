using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.ValueObject;

/// <summary>
/// 值对象：DDD：没有任何标识,它们是不可变的。
/// 使用值对象的好处：1.代码更内聚，体现关系的紧密；2.为了更好的复用该类；3.可以为该类增加一些操作方法
/// </summary>
[Owned]
public record Geo
{
    public double Longitude { get; init; }

    public double Latitude { get; init; }

    public Geo(double longitude, double latitude)
    {
        if (longitude < -180 || longitude > 180)
        {
            throw new ArgumentException("Invalid Longitude");
        }
        if (latitude < -90 || latitude > 90)
        {
            throw new ArgumentException("Invalid Latitude");
        }
        Longitude = longitude;
        Latitude = latitude;
    }

}