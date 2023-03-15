using DDDEFCoreOfRicherModel.ValueObject;

namespace DDDEFCoreOfRicherModel.Models;

/// <summary>
/// ÇøÓò
/// </summary>
public record Region : BaseEntity
{
    public long Id { get; init; }
    public MultilingualString Name { get; init; }
    public Area Area { get; private set; }
    public RegionLevel Level { get; private set; }
    public long? Population { get; private set; }
    public Geo Location { get; init; }

    private Region()
    {

    }

    public Region(MultilingualString name, Area area, Geo location, RegionLevel level)
    {
        this.Name = name;
        this.Area = area;
        this.Location= location;
        this.Level = level;
    }

    public void ChangePopulation(long value)
    {
        if (value < 0 )
        {
            throw new ArgumentException("Invalid Population");
        }
        Population = value;
    }

    public void ChangeLevel(RegionLevel regionLevel)
    {
        this.Level = regionLevel;
    }

    public void ChangeArae(Area area) 
    {
        this.Area = area;
    }
}