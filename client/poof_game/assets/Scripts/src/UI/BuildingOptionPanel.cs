using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class BuildingOptionPanel : GamePanel{

	// Move, upgrade, remove, info
	private Button[] buttons;
    public Building building;

	// TODO Make the menu set inactive when you press outside

	/**
     * Overrides the start functionality 
     * provided by GamePanel
     */

	override public void Start () {
		buttons = RetrieveButtonList ("Buttons");
        building = this.transform.GetComponentInParent<Building>();
		GeneratePanel ();
	}

    /**
     * Overrides the GeneratePanel functionality
     * provided bu GamePanel
     */
	override public void GeneratePanel(){
        FindAndModifyUIElement("Move Button", buttons, () => { building.MoveBuilding();});
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> UpgradePanel.upgradePanel.Show(building));
		FindAndModifyUIElement("Remove Button", buttons, ()=> DestroyPanel.destroyPanel.Show(building));
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}	
}
