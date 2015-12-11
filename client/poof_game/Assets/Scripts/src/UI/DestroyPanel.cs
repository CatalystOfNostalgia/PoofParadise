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
}
