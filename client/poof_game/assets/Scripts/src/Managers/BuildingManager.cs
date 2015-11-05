using UnityEngine;
using System.Collections.Generic;
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

	private Building target;

	// the tile the mouse is currently one
	public Tile selectedTile { get; set; }

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
			target = fireTreeLevel1;
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
			target = fireTreeLevel1;
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
	void deleteBuilding(){
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
		
	//helper method to get mouse position
	private Vector3 getCurrentMousePosition(){
		return Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
	}

	// Use this for initialization
	void Start () {

		Instance ();
		buildingTypeDict = new Dictionary<string, Building>();
		buildingTypeDict.Add ("fire", fireTreeLevel1);
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
