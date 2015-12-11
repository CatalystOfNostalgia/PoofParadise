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
        FindAndModifyUIElement("Move Button", buttons, () => { building.MoveBuilding();});
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> building.UpgradeBuilding ());
		FindAndModifyUIElement("Remove Button", buttons, ()=> building.DeleteBuilding());
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}

    /**
     * Sets the building reference for this panel
     */
    private void SetBuilding()
    {
        building = this.transform.GetComponentInParent<Building>();
    }
}
