/**
 * Object holding information about the base building
 */
public abstract class BuildingInformation{

    private int id;
    private int fireCost;
    private int earthCost;
    private int waterCost;
    private int airCost;
    private int levelRequirement;

    public int ID { get { return id; } }
    public int FireCost { get { return fireCost; } }
    public int EarthCost { get { return earthCost; } }
    public int WaterCost { get { return waterCost; } }
    public int AirCost { get { return airCost; } }
    public int LevelRequirement { get { return levelRequirement; } }

    public BuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int levelRequirement)
    {
        this.id = ID;
        this.fireCost = fireCost;
        this.earthCost = earthCost;
        this.waterCost = waterCost;
        this.airCost = airCost;
        this.levelRequirement = levelRequirement;
    }
}
