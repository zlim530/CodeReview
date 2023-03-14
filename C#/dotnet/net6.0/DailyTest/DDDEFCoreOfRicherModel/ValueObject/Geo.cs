using Microsoft.EntityFrameworkCore;

namespace DDDEFCoreOfRicherModel.ValueObject;

/// <summary>
/// ֵ����DDD��û���κα�ʶ,�����ǲ��ɱ�ġ�
/// ʹ��ֵ����ĺô���1.������ھۣ����ֹ�ϵ�Ľ��ܣ�2.Ϊ�˸��õĸ��ø��ࣻ3.����Ϊ��������һЩ��������
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