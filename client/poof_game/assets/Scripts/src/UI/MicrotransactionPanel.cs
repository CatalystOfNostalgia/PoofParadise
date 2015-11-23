using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class MicrotransactionPanel : GamePanel {

	public static MicrotransactionPanel mp;
	private WoolyBeansPanel wbp;
	
	private Button[] buttons;
	
	override public void Start()
	{
		buttons = RetrieveButtonList("Dialogue Panel/Buttons");
		wbp = GameObject.Find("Wooly Beans Panel(Clone)").GetComponent<WoolyBeansPanel>();
		GeneratePanel();
	}
	
	override public void GeneratePanel()
	{
		FindAndModifyUIElement("Exit Button", buttons, TogglePanel);
		FindAndModifyUIElement("Buy Stuff Button", buttons, testButton);
	}
	
	private void testButton() {
		SaveState.state.woolyBeans += 10;
		wbp.updateBeanCount();
	}
}
