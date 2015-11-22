using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingOptionPanel : GamePanel {

	//move, upgrade, remove, info
	private Button[] buttons;

	//lets make the menu set inactive when you press outside

	// Use this for initialization
	override public void Start () {
		buttons = RetrieveButtonList ("Buttons");
		GeneratePanel ();
	}

	override public void GeneratePanel(){
		FindAndModifyUIElement("Move Button", buttons, ()=> Debug.Log("Move button is pressed"));
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> Debug.Log("Upgrade button is pressed"));
		FindAndModifyUIElement("Remove Button", buttons, ()=> Debug.Log("Remove button is pressed"));
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
