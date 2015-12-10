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

    /**
     * Allows buildings to be re-added to
     * building menu when deleted
     */
    public override void DeleteBuilding()
    {

        base.DeleteBuilding();
        BuildingManager.buildingManager.alreadyPlacedDownBuildings.Remove(this.name.Substring(0, this.name.Length - "(Clone)".Length));
        BuildingPanel.buildingPanel.GeneratePanel();
    }

}
