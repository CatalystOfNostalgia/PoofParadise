using UnityEngine.UI;

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
		FindAndModifyUIElement ("Menu Button", buttons, () => BuildingPanel.buildingPanel.TogglePanel ());
        // TODO: Setup functionality for achievement button
        // TODO: Setup functionality
        FindAndModifyUIElement("Options Button", buttons, () => SettingsMenu.menu.TogglePanel());
    }
}
