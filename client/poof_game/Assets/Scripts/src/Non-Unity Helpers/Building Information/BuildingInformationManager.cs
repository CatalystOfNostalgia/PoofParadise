using System.Collections.Generic;

/**
 * A hardcoded class of the building information
 * which includes the id, cost of buildings
 */
public class BuildingInformationManager {

    private Dictionary<string, ResourceBuildingInformation> resourceBuildingInformationDict;
    private Dictionary<string, DecorationBuildingInformation> decorativeBuildingInformationDict;

    public BuildingInformationManager ()
    {
        resourceBuildingInformationDict = new Dictionary<string, ResourceBuildingInformation>();
        decorativeBuildingInformationDict = new Dictionary<string, DecorationBuildingInformation>();

        addResourceBuildingInfo();
        addDecorativeBuildingInfo();
	}

    public Dictionary<string, ResourceBuildingInformation> ResourceBuildingInformationDict { get { return resourceBuildingInformationDict; } }
    public Dictionary<string, DecorationBuildingInformation> DecorationBuildingInformationDict { get { return decorativeBuildingInformationDict; } }

    //ugh i really should have used builder pattern
    private void addResourceBuildingInfo()
    {
        resourceBuildingInformationDict.Add("Fire Tree Lvl 1", new ResourceBuildingInformation(1, 0, 0, 0, 0, 1, 5, 0, 0, 0));
        resourceBuildingInformationDict.Add("Fire Tree Lvl 2", new ResourceBuildingInformation(2, 100, 0, 0, 0, 2, 10, 0, 0, 0));

        resourceBuildingInformationDict.Add("Pond Lvl 1", new ResourceBuildingInformation(3, 0, 0, 0, 0, 1, 0, 0, 5, 0));
        resourceBuildingInformationDict.Add("Pond Lvl 2", new ResourceBuildingInformation(4, 0, 0, 100, 0, 2, 0, 0, 10, 0));

        resourceBuildingInformationDict.Add("Windmill Lvl 1", new ResourceBuildingInformation(5, 0, 0, 0, 0, 1, 0, 0, 0, 5));
        resourceBuildingInformationDict.Add("Windmill Lvl 2", new ResourceBuildingInformation(6, 0, 0, 0, 100, 2, 0, 0, 0, 10));

        resourceBuildingInformationDict.Add("Cave Lvl 1", new ResourceBuildingInformation(7, 0, 0, 0, 0, 1, 0, 5, 0, 0));
        resourceBuildingInformationDict.Add("Cave Lvl 2", new ResourceBuildingInformation(8, 0, 100, 0, 0, 2, 0, 10, 0, 0));

        resourceBuildingInformationDict.Add("Poof Residence", new ResourceBuildingInformation(9, 50, 50, 50, 50, 1, 0, 0, 0, 0));
    }

    private void addDecorativeBuildingInfo()
    {
        DecorationBuildingInformationDict.Add("Geode", new DecorationBuildingInformation(1, 0, 50, 0, 0, 1, 2));
        DecorationBuildingInformationDict.Add("Volcano", new DecorationBuildingInformation(2, 50, 0, 0, 0, 1, 2));
        DecorationBuildingInformationDict.Add("Waterfall", new DecorationBuildingInformation(3, 0, 0, 50, 0, 2, 4));
        DecorationBuildingInformationDict.Add("Wind Organ", new DecorationBuildingInformation(4, 0, 0, 0, 50, 2, 4));
    }
}
