using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

/**
 * This menu should
 * 1) Allow users to place buildings on the grid
 * 2) Decorate buildings
 */
public class BuildingPanel : GamePanel {
	
	// A static reference to this object
	public static BuildingPanel buildingPanel;
	private Button[] resourceButtons;
    private Button[] decorativeButtons;

    private enum panel : int { DECORATIVE, RESOURCE };
    private panel activePanel;
    private int[,] buildingCostsResource;
    private int[,] buildingCostsDecorative;
    private Texture2D[] icons;
    private int buildingCostResourceCount;
    private int buildingCostDecorativeCount;

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
        activePanel = panel.DECORATIVE;
        SwitchPanels();
	}

    /**
     * A function for switching between the panels
     */
    public void SwitchPanels()
    {
        // Turn off current panel
        this.transform.GetChild((int)activePanel).gameObject.SetActive(false);

        // Set activePanel
        if (activePanel == panel.RESOURCE)
        {
            activePanel = panel.DECORATIVE;
        }
        else
        {
            activePanel = panel.RESOURCE;
        }

        // Turn on current panel
        this.transform.GetChild((int)activePanel).gameObject.SetActive(true);
    }

    /**
     * Adds functionality to all of the buttons on the panel
     */
    override public void GeneratePanel(){
        if (resourceButtons != null)
        {
            foreach(Button button in resourceButtons)
            {
                Destroy(button.gameObject);
            }
        }
        if (decorativeButtons != null)
        {
            foreach(Button button in decorativeButtons)
            {
                Destroy(button.gameObject);
            }
        }

        buildingCostsResource = new int[4, 4];
        buildingCostsDecorative = new int[4, 4];
        icons = Resources.LoadAll("Image/Icon", typeof(Texture2D)).Cast<Texture2D>().ToArray();//sort this shit to fire, water, air, earth
        Array.Sort(icons, new ResourceTypeComparator());
        buildingCostResourceCount = 0;
        buildingCostDecorativeCount = 0;

        // Clear out arrays
        resourceButtons = null;
        decorativeButtons = null;

        // Rebuild them
        resourceButtons = CreateButtons(PrefabManager.prefabManager.resourceBuildings, "Resource Building Panel/Buttons");
        decorativeButtons = CreateButtons(PrefabManager.prefabManager.decorativeBuildings, "Decorative Building Panel/Buttons");

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
        List<GameObject> temp = new List<GameObject>();
        // Mega hardcoded and risky
        foreach (Transform t in transform.GetChild(0).GetChild(0))
        {
            temp.Add(t.gameObject);
        }
        GameObject[] go = temp.ToArray();
        
        // If we are adding buttons to the decorative building panel
        for (int i = 0; i < buildings.Length; i++)
        {
            Button b = MakeButton(path, go[i].transform.position, buildings[i], i);
            //b.transform.SetParent(go[i].transform);
            list.Add(b);
        }
        return list.ToArray();
    }

    /**
     * Generates a button
     */
    public Button MakeButton(string path, Vector3 position, Building b, int index)
    {
        // Build new game object and attach components
        GameObject go = new GameObject();
        Button button = go.AddComponent<Button>();
        Image image = go.AddComponent<Image>();
        ButtonDragScript bds = go.AddComponent<ButtonDragScript>();

        // Attach a text object to the button
        Image buildingInfo = Instantiate(PrefabManager.prefabManager.buildingInfo);
        buildingInfo.transform.SetParent(go.transform);
        buildingInfo.rectTransform.localPosition = new Vector3(0, 40, 0);

        Text text = buildingInfo.GetComponentInChildren<Text>();
        text.text = b.name;
        text.font = (Font)Resources.Load("Font/Candara");
        text.color = Color.black;

        // Attach a cost object to the object
        buildingInfo = Instantiate(PrefabManager.prefabManager.buildingInfo);
        buildingInfo.transform.SetParent(go.transform);
        buildingInfo.rectTransform.localPosition = new Vector3(0, -40, 0);
        Text textCost = buildingInfo.GetComponentInChildren<Text>();
        ResourceBuildingInformation rbi;
        DecorationBuildingInformation dbi;
        if (SaveState.state.buildingInformationManager.ResourceBuildingInformationDict.TryGetValue(b.name, out rbi))
        {
            buildingCostsResource[index,0] = rbi.FireCost;
            buildingCostsResource[index,1] = rbi.WaterCost;
            buildingCostsResource[index,2] = rbi.AirCost;
            buildingCostsResource[index,3] = rbi.EarthCost;
            buildingCostResourceCount++;
        }
        else if (SaveState.state.buildingInformationManager.DecorationBuildingInformationDict.TryGetValue(b.name, out dbi))
        {
            buildingCostsDecorative[index, 0] = dbi.FireCost;
            buildingCostsDecorative[index, 1] = dbi.WaterCost;
            buildingCostsDecorative[index, 2] = dbi.AirCost;
            buildingCostsDecorative[index, 3] = dbi.EarthCost;
            buildingCostDecorativeCount++;
        }
        textCost.font = (Font)Resources.Load("Font/Candara");
        textCost.color = Color.black;

        // Set the name and parent of the game object
        go.transform.SetParent(this.transform.Find(path));
        go.name = b.name;

        // Rect Transform stuff
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(90, 70);

        // Image component stuff
        SpriteRenderer sr = b.GetComponent<SpriteRenderer>();
        image.sprite = sr.sprite;

        button.transform.position = position;
        bds.building = b;
        return button;
    }
    
    private int calculateScreenProportion(double rate, int screen)
    {
        return (int)(screen * rate);
    }

    public void OnGUI()
    {
        switch (activePanel)
        {
            case panel.RESOURCE:
                for (int i = 0; i < buildingCostsResource.GetLength(0) && i<buildingCostResourceCount; i++)
                {
                    drawCost(i, buildingCostsResource);
                }
                break;
            case panel.DECORATIVE:
                for (int i = 0; i < buildingCostsDecorative.GetLength(0) && i<buildingCostDecorativeCount; i++)
                {
                    drawCost(i, buildingCostsDecorative);
                }
                break;
            default:
                Debug.Log("[BuildingPanel] Illegal switch");
                break;
        }
    }

    private void drawCost(int i, int[,] costs)
    {
        GUILayout.BeginArea(new Rect(calculateScreenProportion(.296, Screen.width) + calculateScreenProportion(i, 200), Screen.height - 30, 225, 60));
        GUILayout.BeginHorizontal();
        int textWidth = 20;
        int iconLength = 20;

        GUILayout.Label(icons[0], GUILayout.Width(iconLength), GUILayout.Height(iconLength));
        GUILayout.Label("" + costs[i, 0], GUILayout.MaxWidth(textWidth));
        GUILayout.Label(icons[1], GUILayout.Width(iconLength), GUILayout.Height(iconLength));
        GUILayout.Label("" + costs[i, 1], GUILayout.MaxWidth(textWidth));
        GUILayout.Label(icons[2], GUILayout.Width(iconLength), GUILayout.Height(iconLength));
        GUILayout.Label("" + costs[i, 2], GUILayout.MaxWidth(textWidth));
        GUILayout.Label(icons[3], GUILayout.Width(iconLength), GUILayout.Height(iconLength));
        GUILayout.Label("" + costs[i, 3], GUILayout.MaxWidth(textWidth));

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    /**
* Dynamically creates buttons
* path - supplies the path the the parent for the buttons
* SaveState is never initialized from the Demo Scene, you must start from Login Scene
* TODO: Call this when the user upgrades the hq so that the building menu is refreshed
*/
    public Button[] CreateButtons(Building[] buildingList, string path)
    {
       
        List<Building> list = new List<Building>();
        int i;
        for (i = 0; i < buildingList.Length && list.Count<4; i++) //i < index + 4 not sure what this does
        {
            // Checking if the user can build the building or not
            ResourceBuildingInformation resourceBuildingInfo;
            DecorationBuildingInformation decorationBuildingInfo;

            // SaveState is never initialized from the Demo Scene, you must start from Login Scene
            if (SaveState.state.buildingInformationManager.ResourceBuildingInformationDict.TryGetValue(buildingList[i].name, out resourceBuildingInfo))
            {
                ResourceBuildingLevelCheck(buildingList, list, i, resourceBuildingInfo);
            }
            else if (SaveState.state.buildingInformationManager.DecorationBuildingInformationDict.TryGetValue(buildingList[i].name, out decorationBuildingInfo))
            {
                DecorationBuildingLevelCheck(buildingList, list, i, decorationBuildingInfo);
            }
        }
        return AddButtonsToPanel(list.ToArray(), path);
    }

    /**
     * A function which checks the level requirement of a building
     */
    private void ResourceBuildingLevelCheck(Building[] buildingList, List<Building> list, int i, ResourceBuildingInformation resourceBuildingInfo)
    {
        int levelRequirement = resourceBuildingInfo.LevelRequirement;
        if (levelRequirement == 1 && (! BuildingManager.buildingManager.alreadyPlacedDownBuildings.Contains(buildingList[i].name)))
        {
            list.Add(buildingList[i]);
        }
    }

    /**
     * A function which checks the level requirement of a building
     */
    private void DecorationBuildingLevelCheck(Building[] buildingList, List<Building> list, int i, DecorationBuildingInformation decorationBuildingInfo)
    {
        int levelRequirement = decorationBuildingInfo.LevelRequirement;
        if (SaveState.state.hqLevel >= levelRequirement)
        {
            list.Add(buildingList[i]);
        }
    }
}
