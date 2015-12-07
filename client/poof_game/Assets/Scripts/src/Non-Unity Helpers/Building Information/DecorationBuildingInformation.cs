using UnityEngine;
using System.Collections;

public class DecorationBuildingInformation : BuildingInformation {

    private int poofAttractionRate;

    public DecorationBuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int levelRequirement, int poofAttractionRate) : base(ID, fireCost, earthCost, waterCost, airCost, levelRequirement)
    {
        this.poofAttractionRate = poofAttractionRate;
    }

    public int PoofAttractionRate { get { return poofAttractionRate; } }
}
