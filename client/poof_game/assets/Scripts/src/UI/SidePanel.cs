using UnityEngine.UI;

/**
 * The panel which holds all of the buttons to the left side of the screen
 */
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
		FindAndModifyUIElement ("Menu Button", buttons, () => buildBuildingPanel());
		FindAndModifyUIElement ("Leadership Button", buttons, () => LeaderPanel.leaderPanel.TogglePanel());
        // TODO: Setup functionality for achievement button
        FindAndModifyUIElement("Options Button", buttons, () => SettingsMenu.menu.TogglePanel());
    }

     private void buildBuildingPanel() {
        
        BuildingPanel.buildingPanel.GeneratePanel();
        BuildingPanel.buildingPanel.TogglePanel();
    }
}
