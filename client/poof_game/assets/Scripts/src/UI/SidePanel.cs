using UnityEngine.UI;
using System;

public class SidePanel : GamePanel {

    private Button[] buttons;

    /**
     * Initialize
     */
	override public void Start () {
        buttons = RetrieveButtonList("Buttons");
        GeneratePanel();
	}

    /**
     * Gives all buttons functionality
     */
    public override void GeneratePanel()
    {
        FindAndModifyButton("Menu Button", buttons, () => ModelPanel.modelPanel.Choice("Choose a Button and brace yourself"));
    }
}
