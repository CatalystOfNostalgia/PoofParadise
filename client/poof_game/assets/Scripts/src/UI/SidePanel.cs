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
        FindAndModifyButton("Menu Button", buttons, () => ModelPanel.modelPanel.TogglePanel());
        // TODO: Setup functionality for achievement button
        // TODO: Setup functionality
        FindAndModifyButton("Options Button", buttons, () => SettingsMenu.menu.TogglePanel());
    }
}
