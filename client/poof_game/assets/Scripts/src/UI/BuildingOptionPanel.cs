using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		FindAndModifyUIElement("Remove Button", buttons, ()=> Debug.Log("Remove button is pressed"));
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}
	
    /**
     * Establishes the link between this panel and its building
     */
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
