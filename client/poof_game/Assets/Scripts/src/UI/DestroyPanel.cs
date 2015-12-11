using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class DestroyPanel : GamePanel {
	
	public static DestroyPanel destroyPanel;
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
		FindAndModifyUIElement ("Yes", buttons, ()=> building.DeleteBuilding());
		FindAndModifyUIElement ("No", buttons, ()=> TogglePanel());
	}
}
