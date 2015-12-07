using UnityEngine;
using System.Collections;

public class DecorationBuildingInformation : BuildingInformation {

    private int poofAttractionRate;

    public DecorationBuildingInformation(int ID, int fireCost, int earthCost, int waterCost, int airCost, int poofAttractionRate) : base(ID, fireCost, earthCost, waterCost, airCost)
    {
        this.poofAttractionRate = poofAttractionRate;
    }

    public int PoofAttractionRate { get { return poofAttractionRate; } }
}
