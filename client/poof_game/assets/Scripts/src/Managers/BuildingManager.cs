using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Collections;

public class BuildingManager : Manager {

	/**
	 * just dragging gameobjects to here for now
	 * maybe we can just search them later?
	 */
	public Building windmillLevel1;
	public Building windmillLevel2;
	public Building pondLevel1;
	public Building pondLevel2;
	public Building fireTreeLevel1;
	public Building fireTreeLevel2;
	public Building caveLevel1;
	public Building caveLevel2;

    private ResourceBuilding[] resourceBuildings;

	private Building target;

	// the tile the mouse is currently one
	public Tile selectedTile { get; set; }

	//the dictionary containing all the different types of buildings that can be made
	Dictionary <string, Building> buildingTypeDict;
	Dictionary<Tuple, Building> existingBuildingDict;

	//the dictionary containing buildings on the grid
	public static BuildingManager buildingManager;

	public bool buildingMode;

    /**
     * Initializes BuildingManager as a singleton
     *
     * Initializes fields
     */
    override public void Start()
    {

        if (buildingManager == null)
        {
            DontDestroyOnLoad(gameObject);
            buildingManager = this;
        }
        else if (buildingManager != this)
        {
            Destroy(gameObject);
        }

        buildingTypeDict = new Dictionary<string, Building>();
        buildingTypeDict.Add("fire", fireTreeLevel1);
        existingBuildingDict = new Dictionary<Tuple, Building>();

        resourceBuildings = Resources.LoadAll("Prefabs/Buildings", typeof(ResourceBuilding)).Cast<ResourceBuilding>().ToArray();
    }

    //this overload does nothing right now
    public void dragNewBuilding (int buildingNum, Vector3 cursor){
		buildingMode = true;
		switch (buildingNum) {
		case 1:
			target = resourceBuildings[0];
			break;
		case 2:
			target = pondLevel1;
			break;
		case 3:
			target = caveLevel1;
			break;
		case 4:
			target = windmillLevel1;
			break;
		default:
			target = windmillLevel1;
			break;
		}
	}
	/**
	 * 1. check building cost
	 * 2. see if user has enough resource to cover the cost
	 * 3. decrement resource
	 * 4. build
	 */
	public void makeNewBuilding (int buttonNum){
		buildingMode = true;
		switch (buttonNum) {
		case 1:
			target = resourceBuildings[0];
			break;
		case 2:
			target = pondLevel1;
			break;
		case 3:
			target = caveLevel1;
			break;
		case 4:
			target = windmillLevel1;
			break;
		default:
			target = windmillLevel1;
			break;
		}
	}

	public bool isOccupied (){
		return false;
	}

	// places a building on the currently selected tile
	public void PlaceBuilding(Building prefab) {

		PlaceBuilding (prefab, selectedTile);
	}
	
	// places a building on the given tile
	public void PlaceBuilding (Building prefab, Tile tile) {

		Building newBuilding = tile.PlaceBuilding (prefab);

		// TODO this feels pretty iffy
		if (!SaveState.state.resourceBuildings.ContainsKey (tile.index)) {
			SaveState.state.resourceBuildings.Add (tile.index, newBuilding);
		}
	}
	private bool isTileTaken(Tuple t){
		return SaveState.state.resourceBuildings.ContainsKey (t);

	}

	//rough estimate of distance because sqrt takes a lot of computation
	private float getDistance(float x1, float y1, float x2, float y2){
		return (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
	}

    /**
     * Helper method to get mouse position
     */
    private Vector3 getCurrentMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    /**
     * Builds a sprite list
     *
     * Deprecated -> Use if you wish to generate your own prefabs
     */
    public void GenerateSpriteList(Object[] objs, Sprite[] outList)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            System.Type type = objs[i].GetType();

            if (type == typeof(UnityEngine.Texture2D))
            {

                Texture2D tex = objs[i] as Texture2D;

                Sprite newSprite = Sprite.Create(objs[i] as Texture2D, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);

                outList[i] = newSprite;
            }
        }
    }

    /**
     * Generates building object
     *
     * Deprecated -> Still using prefabs but generating list dynamically
     * instead of storing references
     */ 
    public Building GenerateBuildingObject(int size, string name, int fireCost, int waterCost, int earthCost, int airCost)
    {
        // This line creates an object -> We just want to generate a prefab
        GameObject obj = new GameObject();
        obj.name = name;

        obj.AddComponent<SpriteRenderer>();
        //obj.GetComponent<SpriteRenderer>().sprite = resourceBuildingSprites[1];

        obj.AddComponent<ResourceBuilding>();
        Building ret = obj.transform.GetComponent<ResourceBuilding>();
        ret.size = size;
        ret.buildingName = name;
        ret.fireCost = fireCost;
        ret.waterCost = waterCost;
        ret.earthCost = earthCost;
        ret.airCost = airCost;

        Destroy(obj);

        return ret;
    }
	
	// Update is called once per frame
	void Update () {
		//if (buildingMode && Input.GetMouseButtonDown (0)) {
		if (buildingMode) {
			buildingMode = false;
			if (!target) {
				Debug.Log ("no target");
			}
			PlaceBuilding(target);
			Debug.Log ("building mode set to false");
		}

	}
}
