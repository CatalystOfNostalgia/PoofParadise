using UnityEngine;
using System.Collections.Generic;

/**
 * Handles all building operations
 */
public class BuildingManager : Manager {

    // The target building
	private Building target;

	// The tile the mouse is currently one
	public Tile selectedTile { get; set; }

	// The dictionary containing all the different types of buildings that can be made
	Dictionary <string, Building> buildingTypeDict;
	Dictionary<Tuple, Building> existingBuildingDict;

	// The dictionary containing buildings on the grid
	public List<string> alreadyPlacedDownBuildings { get; set; }
	public static BuildingManager buildingManager;

    // Does a thing
	public bool buildingMode;

    // A convenience object for holding all instantiated buildings
    private GameObject buildings;

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

        alreadyPlacedDownBuildings = new List<string>();
        buildings = new GameObject();
        buildings.name = "Buildings";
        buildingTypeDict = new Dictionary<string, Building>();
        existingBuildingDict = new Dictionary<Tuple, Building>();

    }

	/**
	 * 1. check building cost
	 * 2. see if user has enough resource to cover the cost
	 * 3. decrement resource
	 * 4. build
	 */
	public void makeNewBuilding (Building building){
		buildingMode = true;
        target = building;
	}

	/**
     * Places a building on the currently selected tile
     */
	public void PlaceBuilding(Building prefab) {

		PlaceBuilding (prefab, selectedTile);
	}
	
	/**
     * Places a building on the given tile
     */
	public void PlaceBuilding (Building prefab, Tile tile) {

        // Exit if supplied tile is null
		if (tile == null) {
            Debug.LogError("Cannot place building because tile is null");
            return;
		}
        else {

            Building newBuilding = tile.PlaceBuilding (prefab);

                if (newBuilding != null) {

                    newBuilding.created = true;
                    
                    // Sets the new building's parent to our convenience object
                    newBuilding.transform.SetParent(buildings.transform);

                    // TODO this feels pretty iffy
                    if ( !isTileTaken(tile.index)) {

                        SaveState.state.addBuilding(tile.index, newBuilding);

                        if (prefab.GetComponent<ResidenceBuilding>() == null)
                        {   

                            Debug.Log("placing building: " + prefab.name);
                            alreadyPlacedDownBuildings.Add(prefab.name);
                        }

                    }

                GameManager.gameManager.SpawnPoofs();
            } else {
                Debug.Log("You cannot afford this building");
                return;
            }
        }

	}

    /**
     * TODO: Give description
     */
	private bool isTileTaken(Tuple t){
		return (SaveState.state.resourceBuildings.ContainsKey (t) ||
		       SaveState.state.decorativeBuildings.ContainsKey (t) ||
		       SaveState.state.residenceBuildings.ContainsKey (t));
	}

	/**
     * Rough estimate of distance because sqrt takes a lot of computation
     */
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
     * Update is called once per frame
     */
	void Update () {
		if (buildingMode) {
			buildingMode = false;
			if (!target) {
				Debug.Log ("No target");
			}
			PlaceBuilding(target);
		}
	}
}
