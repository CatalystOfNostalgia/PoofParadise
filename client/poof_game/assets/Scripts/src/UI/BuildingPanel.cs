using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

/**.
 * This menu should
 * 1) Allow users to place buildings on the grid
 * 2) Decorate buildings
 */
public class BuildingPanel : GamePanel {
	
	// A static reference to this object
	public static BuildingPanel buildingPanel;
	
	private Button[] buttons;
	
	public Button exit;
	
	/**
     * Generates references based on children
     */
	override public void Start()
	{
		if (buildingPanel == null) {
			DontDestroyOnLoad(gameObject);
			buildingPanel = this;
		} 
		else if (buildingPanel != this) {
			Destroy(gameObject);
		}
	    buttons = RetrieveButtonList("Dialogue Panel/Buttons");
		GeneratePanel();
	}
	
	/**
     * Adds functionality to all of the buttons on the panel
     */
	override public void GeneratePanel(){
		foreach (Button b in buttons)
		{
			b.onClick.RemoveAllListeners();
			b.onClick.AddListener(TogglePanel);
		}

		/**FindAndModifyUIElement("Fire Tree Button", buttons, () => BuildingManager.buildingManager.PlaceBuilding("Fire Tree Button"));
		
		FindAndModifyUIElement("Pond Button", buttons, () => BuildingManager.buildingManager.PlaceBuilding("Pond Button"));
		
		FindAndModifyUIElement("Cave Button", buttons, () => BuildingManager.buildingManager.PlaceBuilding("Cave Button"));
		
		FindAndModifyUIElement("Windmill Button", buttons, () => BuildingManager.buildingManager.PlaceBuilding("Windmill Button"));**/
		
	}
	
    /**
     * Dynamically creates buttons
     */
	public void CreateButtons()
    {
        List<Button> list = new List<Button>();
        foreach (Building b in PrefabManager.prefabManager.resourceBuildings)
        {
            SpriteRenderer i = b.GetComponent<SpriteRenderer>();

        }
    }
}
