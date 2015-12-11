using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class UpgradePanel : BuildingOptionPanel{

	public static UpgradePanel upgradePanel;
	public Button[] buttons; 

	override public void Start(){
		buttons = RetrieveButtonList ("Buttons");
		GeneratePanel ();
		setFont ();
	}
	public void getBuilding(Building target){
		this.building = target;
	}
	override public void GeneratePanel(){
		FindAndModifyUIElement ("Select Resources", buttons, ()=> building.UpgradeBuilding());
		FindAndModifyUIElement ("Select Wooly Beans", buttons, ()=> Debug.Log("upgrade with wooly beans"));
		FindAndModifyUIElement ("Exit", buttons, ()=> upgradePanel.TogglePanel());
	}
	public void setFont(){
		Text text = upgradePanel.GetComponentInChildren<Text>();
		text.text = "Select an option to upgrade";
		text.font = (Font)Resources.Load("Font/Candara");
		text.color = Color.black;
	}
}
