using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : Manager {

    // The target building
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
        existingBuildingDict = new Dictionary<Tuple, Building>();

    }

    //this overload does nothing right now
    public void dragNewBuilding (int buildingNum, Vector3 cursor){
		buildingMode = true;
        target = PrefabManager.prefabManager.resourceBuildings[buildingNum];
	}
	/**
	 * 1. check building cost
	 * 2. see if user has enough resource to cover the cost
	 * 3. decrement resource
	 * 4. build
	 */
	public void makeNewBuilding (int buttonNum){
		buildingMode = true;
        target = PrefabManager.prefabManager.resourceBuildings[buttonNum];
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

		if (tile == null) {
			Debug.Log ("tile is null");
		}

		Building newBuilding = tile.PlaceBuilding (prefab);
        newBuilding.created = true;

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
