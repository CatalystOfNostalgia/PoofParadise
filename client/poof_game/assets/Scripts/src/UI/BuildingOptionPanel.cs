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
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> Debug.Log("Upgrade button is pressed"));
		FindAndModifyUIElement("Remove Button", buttons, ()=> removeBuilding());
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}
	
    /**
     * Establishes the link between this panel and its building
     */
	//comment in the resource checks once that is sorted out
	/**private void upgradeBuilding()
	{
		string name = this.transform.GetComponentInParent<Building>().name;
		if (name.Contains ("Lvl 1")) {
			if (name.Contains ("Cave")) {
				//if(SaveState.state.earth>= 10){
				SaveState.state.earth = SaveState.state.earth - 10;
				int num = 0; //location on the panel
				int ID = this.transform.GetComponentInParent<Building> ().ID;
				Destroy (this.transform.GetComponentInParent<Building>().gameObject);
				BuildingManager.buildingManager.makeNewBuilding (num + 1);
				//}
			}
			if (name.Contains ("Fire")) {
				//if(SaveState.state.fire>= 10){
				SaveState.state.fire = SaveState.state.fire - 10;
				int num = this.transform.GetComponentInParent<Building> ().ID;
				Destroy (this.transform.GetComponentInParent<Building>().gameObject);
				BuildingManager.buildingManager.makeNewBuilding (num + 1);
				//}
			}
			if (name.Contains ("Pond")) {
				//if(SaveState.state.water>= 10){
				SaveState.state.water = SaveState.state.water - 10;
				int num = this.transform.GetComponentInParent<Building> ().ID;
				Destroy (this.transform.GetComponentInParent<Building>().gameObject);
				BuildingManager.buildingManager.makeNewBuilding (num + 1);
				//}
			}
			if (name.Contains ("Windmill")) {
				//if(SaveState.state.air>= 10){
				SaveState.state.air = SaveState.state.air - 10;
				int num = this.transform.GetComponentInParent<Building> ().ID;
				Destroy (this.transform.GetComponentInParent<Building>().gameObject);
				BuildingManager.buildingManager.makeNewBuilding (num + 1);
				//}
			}
		} else {
			errorBuild.gameObject.SetActive(true);
		}
	}**/
	/**removes only decorative building**/
	private void removeBuilding()
	{
        Building b = this.transform.GetComponentInParent<Building>();
        int Id = b.ID; 
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
        BuildingPanel.buildingPanel.alreadyPlacedDownBuildings.Remove(b.name.Substring(0, b.name.Length - "(Clone)".Length));
        BuildingPanel.buildingPanel.GeneratePanel();
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
