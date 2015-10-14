using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	/**
	 * just dragging gameobjects to here for now
	 * maybe we can just search them later?
	 */
	public Building windmill;
	public Building pond;
	public Building fire;
	public Building cave;

	private Building target;

	// the tile the mouse is currently one
	public Tile selectedTile { get; set; }

	ArrayList buildings;
	//the dictionary containing all the different types of buildings that can be made
	Dictionary <string, Building> buildingTypeDict;
	Dictionary<Tuple, Building> existingBuildingDict;
	//the dictionary containing buildings on the grid
	public static BuildingManager manager;

	public bool buildingMode;
	
	public static BuildingManager Instance(){
		if (!manager) {
			manager = FindObjectOfType(typeof (BuildingManager)) as BuildingManager;
			if(!manager)
				Debug.LogError ("There needs to be one active BuildingManager script on a GameObject in your scene.");
		}
		return manager;
	}

	//this overload does nothing right now
	public void dragNewBuilding (int buildingNum, Vector3 cursor){
		buildingMode = true;
		switch (buildingNum) {
		case 1:
			target = fire;
			break;
		case 2:
			target = pond;
			break;
		case 3:
			target = cave;
			break;
		case 4:
			target = windmill;
			break;
		default:
			target = windmill;
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
			target = fire;
			break;
		case 2:
			target = pond;
			break;
		case 3:
			target = cave;
			break;
		case 4:
			target = windmill;
			break;
		default:
			target = windmill;
			break;
		}
	}
	void deleteBuilding(){
	}

	public bool isOccupied (){
		return false;
	}
	/**
	 * 1. get the closest tile at cursor location
	 * 		a. if too far away, noop
	 * 2. Check to see if there is already a building at the tile
	 * 		a. if true, noop
	 * 		b. if there aren't any building, claim the tile(s)
	 * 3. Place building at the tile
	 * 		a. instantiate the game object
	 * 4. Allow user to cancel
	 */
	private Building PlaceBuilding(Building prefab) {
		Vector3 mousePosition = getCurrentMousePosition ();

		Tile tile = selectedTile;
		//Tile tile = closestTile (mousePosition);
		Tuple tuple = tile.index;
		
		// Hard codin some shit that should be in a method by itself
		//		makes sure you can't over lap buildings
		Tuple l = new Tuple (tile.leftTile.index.x, tile.leftTile.index.y);
		Tuple d = new Tuple (tile.downTile.index.x, tile.downTile.index.y);
		Tuple dl = new Tuple (tile.downLeftTile.index.x, tile.downLeftTile.index.y);
		
        if (!isTileTaken (tuple)) {
            Debug.Log("You just created a building");
            Building newBuilding = Instantiate (prefab, new Vector3(tile.transform.position.x, tile.transform.position.y - .65f, tile.transform.position.y - .65f), Quaternion.identity) as Building;
            if (newBuilding == null)
            {
                Debug.Log("Failed to save instantiated object");
            }

			SaveState.state.existingBuildingDict.Add(tile.index, newBuilding);

			// commenting this out so it only saves a building once.
			/*
            SaveState.state.existingBuildingDict.Add(tuple, newBuilding);
            
			SaveState.state.existingBuildingDict.Add(d, newBuilding);
			SaveState.state.existingBuildingDict.Add(l, newBuilding);
			SaveState.state.existingBuildingDict.Add(dl, newBuilding);

			*/
			
			Debug.Log(SaveState.state.existingBuildingDict[tuple]);
			
			tile.isVacant = false; // Paints placed tile red
			tile.leftTile.isVacant = false; // and nearby 3 tiles
			tile.downTile.isVacant = false;
			tile.downLeftTile.isVacant = false;
			
            //Debug.Log(newBuilding.ToJSON());
			return newBuilding;
		}
		return null;
	}

	// TODO this might not be needed anymore with the new way of getting the closest tile.

	private Tile closestTile (Vector3 mousePos){
		Tile closestTile = null;
		float closestDistance = 0;
		//is there better algorithm for getting the tile that is closest to the cursor?
		foreach(Tile t in TileScript.grid.GetComponentsInChildren<Tile>()){
			float distance = getDistance(mousePos.x, mousePos.y, t.transform.position.x, t.transform.position.y);
			if (closestTile ==null){
				closestTile = t;
				closestDistance = distance;
			}
			else if ( distance < closestDistance){
				closestTile = t;
				closestDistance = distance;
			}
		}
		return closestTile;
	}

	private bool isTileTaken(Tuple t){
		return SaveState.state.existingBuildingDict.ContainsKey (t);

	}

	//rough estimate of distance because sqrt takes a lot of computation
	private float getDistance(float x1, float y1, float x2, float y2){
		return (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
	}
		
	//helper method to get mouse position
	private Vector3 getCurrentMousePosition(){
		return Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	// Use this for initialization
	void Start () {

		Instance ();
		buildingTypeDict = new Dictionary<string, Building>();
		buildingTypeDict.Add ("fire", fire);
		existingBuildingDict = new Dictionary<Tuple, Building>();

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
