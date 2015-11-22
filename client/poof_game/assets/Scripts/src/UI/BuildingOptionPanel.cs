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
		FindAndModifyUIElement("Move Button", buttons, ()=> ); 
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> ); 
		FindAndModifyUIElement("Remove Button", buttons, ()=> ); 
		FindAndModifyUIElement("Info Button", buttons, ()=> ); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
