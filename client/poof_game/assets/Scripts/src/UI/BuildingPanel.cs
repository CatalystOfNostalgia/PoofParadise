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
	
	private Button[] resourceButtons;
    private Button[] decorativeButtons;

    private int resourceIndex;
    private int decorativeIndex;

    private enum panel : int { DECORATIVE, RESOURCE };
    private panel activePanel; 

    public Button prefab;
	
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
        decorativeIndex = 0;
        resourceIndex = 0;
        activePanel = panel.RESOURCE;
        this.transform.GetChild((int)activePanel).gameObject.SetActive(true);
        resourceButtons = CreateButtons(PrefabManager.prefabManager.resourceBuildings, ref resourceIndex, "Resource Building Panel/Buttons");
        decorativeButtons = CreateButtons(PrefabManager.prefabManager.decorativeBuildigs, ref decorativeIndex, "Decorative Building Panel/Buttons");
		GeneratePanel();
	}
	
	/**
     * Adds functionality to all of the buttons on the panel
     */
	override public void GeneratePanel(){
		foreach (Button b in resourceButtons)
		{
			b.onClick.RemoveAllListeners();
			b.onClick.AddListener(TogglePanel);
		}
        foreach (Button b in decorativeButtons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(TogglePanel);
        }
	}

    /**
     * Generates buttons per panel
     */
    public Button[] AddButtonsToPanel(Building[] buildings, string path)
    {
        List<Button> list = new List<Button>();
        // If we are adding buttons to the decorative building panel
        for (int i = 0; i < buildings.Length; i++)
        {
            // TODO: place buttons under the building info assets
            list.Add(MakeButton(path, new Vector3(i * 100 + this.transform.position.x - (buildings.Length * 100 / 2), 100), buildings[i]));
        }
        return list.ToArray();
    }

    public Button MakeButton(string path, Vector3 position, Building b)
    {
        SpriteRenderer sr = b.GetComponent<SpriteRenderer>();
        Button button = (Button)Instantiate(prefab);
        button.transform.SetParent(this.transform.Find(path));
        button.image.sprite = sr.sprite;
        button.image.color = Color.white;
        button.name = b.name;
        button.GetComponentInChildren<Text>().text = b.name;
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 120);// Set(i * 100 + 50, 50, 140, 120);
        button.transform.position = position; //;
        button.gameObject.AddComponent<ButtonDragScript>().ID = b.ID;
        return button;
    }
	
    /**
     * Dynamically creates buttons
     * path - supplies the path the the parent for the buttons
     */
	public Button[] CreateButtons(Building[] buildingList, ref int index, string path)
    {
       
        List<Building> list = new List<Building>();
        int i;
        for (i = index; i < buildingList.Length && i < index + 4; i++)
        {
            list.Add(buildingList[i]);
        }
        index = i;
        return AddButtonsToPanel(list.ToArray(), path);
    }
}
