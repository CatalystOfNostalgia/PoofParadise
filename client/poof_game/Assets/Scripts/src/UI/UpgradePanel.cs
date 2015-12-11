using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class UpgradePanel : GamePanel{

	public static UpgradePanel upgradePanel;
	public Button[] buttons;
    public Building building;

	override public void Start(){
		GeneratePanel ();
	}

	override public void GeneratePanel(){
        buttons = RetrieveButtonList("Buttons");
        FindAndModifyUIElement ("Select Resources", buttons, ()=> UpgradeBuilding());
		FindAndModifyUIElement ("Select Wooly Beans", buttons, ()=> Debug.Log("upgrade with wooly beans"));
		FindAndModifyUIElement ("Exit", buttons, ()=> upgradePanel.TogglePanel());
	}

    public void Show(Building target)
    {
        this.building = target;
        GeneratePanel();
        TogglePanel();
    }

    private void UpgradeBuilding()
    {
        building.UpgradeBuilding();
        TogglePanel();
    }

}
