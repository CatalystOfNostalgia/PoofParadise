using UnityEngine;
using System.Collections;

public class ResourceBuildingInformation : BuildingInformation{

    private int fireGenerationRate;
    private int earthGenerationRate;
    private int waterGenerationRate;
    private int airGenerationRate;

    public ResourceBuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int fireGenerationRate, int earthGenerationRate, int waterGenerationRate, int airGenerationRate) : base(ID, fireCost, earthCost, waterCost, airCost)
    {
        this.fireGenerationRate = fireGenerationRate;
        this.earthGenerationRate = earthGenerationRate;
        this.waterGenerationRate = waterGenerationRate;
        this.airGenerationRate = airGenerationRate;
    }

    public int FireGenerationRate { get { return fireGenerationRate; } }
    public int EarthGenerationRate { get { return earthGenerationRate; } }
    public int WaterGenerationRate { get { return waterGenerationRate; } }
    public int AirGenerationRate { get { return airGenerationRate; } }
}
