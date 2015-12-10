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

	override public void Start(){
		buttons = RetrieveButtonList ("Buttons");
		GeneratePanel ();
	}
	override public void GeneratePanel(){
		FindAndModifyUIElement ("Select Resource", buttons, ()=> Debug.Log("upgrade was clicked with resources"));
		FindAndModifyUIElement ("Select Wooly Beans", buttons, ()=> Debug.Log ("upgrade was clicked with wooly beans"));
		FindAndModifyUIElement ("Exit", buttons, ()=> Debug.Log ("exit was clicked"));
	}
}
