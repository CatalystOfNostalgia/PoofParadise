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
		setFont ();
	}
	
	override public void GeneratePanel(){
        buttons = RetrieveButtonList("Buttons");
        FindAndModifyUIElement ("Select Resources", buttons, ()=> UpgradeBuilding());
		FindAndModifyUIElement ("Select Wooly Beans", buttons, ()=> Toast.toast.makeToast("You do not have enough Wooly Beans"));
		FindAndModifyUIElement ("Exit", buttons, ()=> TogglePanel());
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

	public void setFont(){
		Text text = upgradePanel.GetComponentInChildren<Text>();
		text.text = "Select an option to upgrade";
		text.font = (Font)Resources.Load("Font/Candara");
		text.color = Color.black;
	}
}
