using UnityEngine;
using System.Collections;

public class ResourceBuildingInformation : BuildingInformation{
    public ResourceBuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int fireGenerationRate, int earthGenerationRate, int waterGenerationRate, int airGenerationRate) : base(ID, fireCost, earthCost, waterCost, airCost)
    {
        this.FireGenerationRate = fireGenerationRate;
        this.EarthGenerationRate = earthGenerationRate;
        this.WaterGenerationRate = waterGenerationRate;
        this.AirGenerationRate = airGenerationRate;
    }

    public int FireGenerationRate { get;}
    public int EarthGenerationRate { get;}
    public int WaterGenerationRate { get;}
    public int AirGenerationRate { get;}
}
