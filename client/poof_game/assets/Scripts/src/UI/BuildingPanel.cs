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

    private int activePanel;

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
        activePanel = 1;
        this.transform.GetChild(activePanel).gameObject.SetActive(true);
        resourceButtons = CreateButtons("Resource Building Panel/Buttons", PrefabManager.prefabManager.resourceBuildings);
        decorativeButtons = CreateButtons("Decorative Building Panel/Buttons", PrefabManager.prefabManager.decorativeBuildigs);
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
     * Dynamically creates buttons
     * path - supplies the path the the parent for the buttons
     */
	public Button[] CreateButtons(string path, Building[] buildingList)
    {
        List<Button> list = new List<Button>();
        int i = 0;
        foreach (Building b in buildingList)
        {
                SpriteRenderer sr = b.GetComponent<SpriteRenderer>();
                Button button = (Button)Instantiate(prefab);
                button.transform.SetParent(this.transform.Find(path));
                button.image.sprite = sr.sprite;
                button.image.color = Color.white;
                button.name = b.name;
                button.GetComponentInChildren<Text>().text = b.name;
                button.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 120);// Set(i * 100 + 50, 50, 140, 120);
                button.transform.position = new Vector3(i * 100 + this.transform.position.x - (buildingList.Length*100/2), 100);
                button.gameObject.AddComponent<ButtonDragScript>().ID = b.ID;
                i++;
        }
        return list.ToArray();
    }
}
