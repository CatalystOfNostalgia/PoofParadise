using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Object holding information about the base building
/// </summary>
public abstract class BuildingInformation{
    public int ID { get;}
    public int FireCost { get;}
    public int EarthCost { get;}
    public int WaterCost { get;}
    public int AirCost { get;}

    public BuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost)
    {
        this.ID = ID;
        this.FireCost = fireCost;
        this.EarthCost = earthCost;
        this.WaterCost = waterCost;
        this.AirCost = airCost;
    }
}
