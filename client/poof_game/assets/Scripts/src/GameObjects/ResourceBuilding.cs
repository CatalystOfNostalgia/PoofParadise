/**
 * The Resource Building MonoBehavior extends Building
 * It is a specific type of building which generates resources
 * for the user to spend on decorations that can draw more
 * poof's to the user's paradise
 */
public class ResourceBuilding : Building {

	public enum ResourceType { fire, water, air, earth };

	// the resource collection rate and the type of resource
	public int collectionRate { get; set;}
	public ResourceType type;

	/**This code is breaking delete**/
    /**
     * Allows buildings to be re-added to
     * building menu when deleted
     */
<<<<<<< HEAD
    /**public override void DeleteBuilding()
    {
        base.DeleteBuilding();
        BuildingManager.buildingManager.alreadyPlacedDownBuildings.Remove(this.name.Substring(0, this.name.Length - "(Clone)".Length));
        BuildingPanel.buildingPanel.GeneratePanel();
    }**/
=======
    public override bool DeleteBuilding()
    {
        bool test = base.DeleteBuilding();
        if (test)
        {
            BuildingManager.buildingManager.alreadyPlacedDownBuildings.Remove(BuildingPanel.SubstringClonedBuilding(this.name));
            BuildingPanel.buildingPanel.GeneratePanel();
        }
        return test;
    }
>>>>>>> d2fac759e835ea9263dda3998e7196c986bc16af

    /**
     * Overrides t
     */
    public override bool UpgradeBuilding()
    {
        bool test = false;
        if (name.Contains("Lvl 1") && SaveState.state.hqLevel == 2)
        {
            test = base.UpgradeBuilding();
        }
        return test;
    }

}
