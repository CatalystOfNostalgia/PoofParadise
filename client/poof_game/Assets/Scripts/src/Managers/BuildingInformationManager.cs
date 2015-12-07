using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// A hardcoded class of the building information
/// which includes the id, cost of buildings
/// </summary>
public class BuildingInformationManager : MonoBehaviour {

    public static BuildingInformationManager buildingInformationManager;

    private Dictionary<string, BuildingInformation> informationDict;
	// Use this for initialization
	void Start ()
    {
        informationDict = new Dictionary<string, BuildingInformation>();
	}
	
    private void addResourceBuildingInfo()
    {
        informationDict.Add("Cave Lvl 1", new ResourceBuildingInformation(1, 0, 100, 0, 0, 0, 10, 0, 0));
        informationDict.Add("Fire Tree Lvl 1", new ResourceBuildingInformation(2, 100, 0, 0, 0, 10, 0, 0, 0));
        informationDict.Add("Pond Lvl 1", new ResourceBuildingInformation(3, 0, 0, 100, 0, 0, 0, 10, 0));
        informationDict.Add("Windmill Lvl 1", new ResourceBuildingInformation(4, 0, 0, 0, 100, 0, 0, 0, 10));

    }
}
