using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class BuildingOptionPanel : GamePanel {

	// Move, upgrade, remove, info
	private Button[] buttons;
    private Building building;

	// TODO Make the menu set inactive when you press outside

	/**
     * Overrides the start functionality 
     * provided by GamePanel
     */
	override public void Start () {
		buttons = RetrieveButtonList ("Buttons");
        SetBuilding();
		GeneratePanel ();
	}

    /**
     * Overrides the GeneratePanel functionality
     * provided bu GamePanel
     */
	override public void GeneratePanel(){
        FindAndModifyUIElement("Move Button", buttons, () => { building.MoveBuilding(); gameObject.SetActive(false); });
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> upgradeBuilding ());
		FindAndModifyUIElement("Remove Button", buttons, ()=> building.DeleteBuilding());
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}

	/**
     * A helper method that finds the building
     */
	public Building getNewBuilding(ResourceBuilding[] list, string find){
		for (int i=0; i<=list.Length; i++) {
			if(list[i].name == find){
				return list[i];
			}
		}
		return null;
	}

    /**
     * Upgrades building to level 2 resource building 
     */
	private void upgradeBuilding()
	{
		ResourceBuilding[] resourceBuildings = PrefabManager.prefabManager.resourceBuildings;
		string name = this.transform.GetComponentInParent<Building>().name;
		if (name.Contains ("Lvl 1") && SaveState.state.hqLevel == 2) {
			if (name.Contains ("Cave") && SaveState.state.earth >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Cave Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				building.DeleteBuilding();
				SaveState.state.earth = SaveState.state.earth - 50;
			}
			if (name.Contains ("Fire") && SaveState.state.fire >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Fire Tree Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				building.DeleteBuilding();
				SaveState.state.fire = SaveState.state.fire - 50;
			}
			if (name.Contains ("Pond") && SaveState.state.water >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Pond Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				building.DeleteBuilding();
				SaveState.state.fire = SaveState.state.fire - 50;
			}
			if (name.Contains ("Windmill") && SaveState.state.air >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Windmill Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				building.DeleteBuilding();
				SaveState.state.air = SaveState.state.air - 50;
			}
		} 
	}

    /**
     * Sets the building reference for this panel
     */
    private void SetBuilding()
    {
        building = this.transform.GetComponentInParent<Building>();
    }
}
