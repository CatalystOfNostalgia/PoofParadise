using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; 

public class BuildingOptionPanel : GamePanel {

	//move, upgrade, remove, info
	private Button[] buttons;
    private Building building;

	//lets make the menu set inactive when you press outside

	// Use this for initialization
	override public void Start () {
		buttons = RetrieveButtonList ("Buttons");
        SetBuilding();
		GeneratePanel ();
	}

	override public void GeneratePanel(){
		FindAndModifyUIElement("Move Button", buttons, ()=> Move());
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> upgradeBuilding ());
		FindAndModifyUIElement("Remove Button", buttons, ()=> removeBuilding());
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}

	/**helper method that finds the building**/
	public Building getNewBuilding(ResourceBuilding[] list, string find){
		for (int i=0; i<=list.Length; i++) {
			if(list[i].name == find){
				return list[i];
			}
		}
		return null;
	}
	//upgrades building to level 2 resource building 
	private void upgradeBuilding()
	{
		ResourceBuilding[] resourceBuildings = PrefabManager.prefabManager.resourceBuildings;
		string name = this.transform.GetComponentInParent<Building>().name;
		if (name.Contains ("Lvl 1") && SaveState.state.hqLevel == 2) {
			if (name.Contains ("Cave") && SaveState.state.earth >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Cave Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				removeBuilding();
				SaveState.state.earth = SaveState.state.earth - 50;
			}
			if (name.Contains ("Fire") && SaveState.state.fire >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Fire Tree Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				removeBuilding();
				SaveState.state.fire = SaveState.state.fire - 50;
			}
			if (name.Contains ("Pond") && SaveState.state.water >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Pond Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				removeBuilding();
				SaveState.state.fire = SaveState.state.fire - 50;
			}
			if (name.Contains ("Windmill") && SaveState.state.air >= 50) {
				Building upgraded = getNewBuilding(resourceBuildings, "Windmill Lvl 2");
				BuildingManager.buildingManager.makeNewBuilding(upgraded);
				removeBuilding();
				SaveState.state.air = SaveState.state.air - 50;
			}
		} 
	}
	/**removes only decorative building**/
	private void removeBuilding()
	{ 
	    int Id = this.transform.GetComponentInParent<Building> ().ID; 
		int lengthX = TileScript.grid.gridX;
		int lengthY = TileScript.grid.gridY ;
		int x, y;
		if (Id == 0) {
			x = 0;
			y = 0;
		} 
		else {
			x = lengthX % Id;
			y = lengthY % Id - 1; 
		}
		Tuple position = new Tuple (x, y);
		bool remove = SaveState.state.buildings.Remove(position);
		Destroy (this.transform.GetComponentInParent<Building> ().gameObject);

	}
    private void SetBuilding()
    {
        building = this.transform.GetComponentInParent<Building>();
    }

    /**
     * Provides the move functionality for buildings
     */
    private void Move()
    {
        building.canDrag = true;
        this.gameObject.SetActive(false);
        //TogglePanel();
    }
}
