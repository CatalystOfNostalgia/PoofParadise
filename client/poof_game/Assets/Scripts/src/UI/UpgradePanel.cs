using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class UpgradePanel : GamePanel {

	public static UpgradePanel upgradePanel;
	public Button[] buttons; 
	public Building building;

	override public void Start(){
		buttons = RetrieveButtonList ("Buttons");
		GeneratePanel ();
	}

	public void getBuilding(Building target){
		this.building = target;
	}
	override public void GeneratePanel(){
		FindAndModifyUIElement ("Select Resources", buttons, ()=> building.UpgradeBuilding());
		FindAndModifyUIElement ("Select Wooly Beans", buttons, ()=> Debug.Log("upgrade was clicked with wooly beans"));
		FindAndModifyUIElement ("Exit", buttons, ()=> upgradePanel.TogglePanel());
	}
}
