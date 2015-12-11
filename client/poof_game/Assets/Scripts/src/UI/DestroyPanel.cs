using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class DestroyPanel : GamePanel {
	
	public static DestroyPanel destroyPanel;
	public Button[] buttons; 
	public Building building;
	
	override public void Start(){
		GeneratePanel ();
		setFont ();
	}

	override public void GeneratePanel(){
        buttons = RetrieveButtonList("Buttons");
        FindAndModifyUIElement ("Yes", buttons, ()=> DestroyBuilding());
		FindAndModifyUIElement ("No", buttons, ()=> TogglePanel());
	}

    public void Show(Building target)
    {
        this.building = target;
        GeneratePanel();
        TogglePanel();
    }

    private void DestroyBuilding()
    {
        building.DeleteBuilding();
        TogglePanel();
    }
	
	public void setFont(){
		Text text = destroyPanel.GetComponentInChildren<Text>();
		text.text = "Are you sure you want to destroy this building?";
		text.font = (Font)Resources.Load("Font/Candara");
		text.color = Color.black;
		
	}
}
