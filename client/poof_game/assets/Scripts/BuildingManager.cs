using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	/**
	 * just dragging gameobjects to here for now
	 * maybe we can just search them later?
	 */
	public Building tree;
	public TileScript grid;

	ArrayList buildings;
	//the dictionary containing all the different types of buildings that can be made
	Dictionary <string, Building> buildingTypeDict;
	//the dictionary containing buildings on the grid
	Dictionary <Tuple, Building> existingBuildingDict;
	private static BuildingManager buildingManager;
	
	public static BuildingManager Instance(){
		if (!buildingManager) {
			buildingManager = FindObjectOfType(typeof (BuildingManager)) as BuildingManager;
			if(!buildingManager)
				Debug.LogError ("There needs to be one active BuildingManager script on a GameObject in your scene.");
		}
		return buildingManager;
	}

	/**
	 * 1. check building cost
	 * 2. see if user has enough resource to cover the cost
	 * 3. decrement resource
	 * 4. build
	 */
	public void makeNewBuilding (Vector3 mousePosition){
		PlaceBuilding (tree.gameObject, mousePosition);
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
	private void PlaceBuilding(GameObject target, Vector3 mousePosition) {
		Transform tile = closestTile (mousePosition);
		if (!isTileTaken (new Tuple (0, 0))) {
			Building newBuilding = (Building)Instantiate (target, tile.position, Quaternion.identity);
			newBuilding.transform.position = tile.position;
			existingBuildingDict.Add (new Tuple (0, 0), newBuilding);//place holder tuple for now
		}
	}

	private Transform closestTile (Vector3 mousePos){
		Transform closestTile = null;
		float closestDistance = 0;
		//is there better algorithm for getting the tile that is closest to the cursor?
		foreach(Transform t in grid.GetComponentsInChildren<Transform>()){
			float distance = getDistance(mousePos.x, mousePos.y, t.position.x, t.position.y);
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
		return false;
		//return existingBuildingDict.ContainsKey (t);

	}

	//rough estimate of distance because sqrt takes a lot of computation
	private float getDistance(float x1, float y1, float x2, float y2){
		return (x1-x2)*(x1-x2) + (y1-y2)*(y1-y2);
	}

	// Use this for initialization
	void Start () {
		buildingTypeDict = new Dictionary<string, Building>();
		buildingTypeDict.Add ("tree", tree);
		buildings = new ArrayList ();
		existingBuildingDict = new Dictionary<Tuple, Building> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
