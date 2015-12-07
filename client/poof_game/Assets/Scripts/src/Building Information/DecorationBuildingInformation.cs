using UnityEngine;
using System.Collections;

public class DecorationBuildingInformation : BuildingInformation {
    public DecorationBuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int poofAttractionRate) : base(ID, fireCost, earthCost, waterCost, airCost)
    {
        this.PoofAttractionRate = poofAttractionRate;
    }

    public int PoofAttractionRate { get; set; }
}
