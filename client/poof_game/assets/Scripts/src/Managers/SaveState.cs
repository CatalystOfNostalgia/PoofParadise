using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SaveState : Manager {
            
	// Allows the scene to access this object without searching for it
	public static SaveState state;

	// player data
    public string username { get; set; }
    public string userPassword { get; set; }
	public int userID { get; set; }
	public int userLevel { get; set; }
	public int userExperience { get; set; }
	public int hqLevel { get; set; }
	public int poofCount { get; set; }

	// resources
	public int fire { get; set; }
	public int air { get; set; }
	public int water { get; set; }
	public int earth { get; set; }
	public int maxFire { get; set; }
	public int maxAir { get; set; }
	public int maxWater { get; set; }
	public int maxEarth { get; set; }

	// elemari
	public int fireEle { get; set; }
	public int waterEle { get; set; }
	public int earthEle { get; set; }
	public int airEle { get; set; }

    // poofs
    public int totalPoofs { get; set; }
    public int maxPoofs { get; set; }

	// buildings
	//TODO do we actually need separate dictionaries for the different building type?
	public Dictionary<Tuple, Building> buildings { get; set; }

	//resource collection fields
	public int firetreeRes { get; set; }
	public int windmillRes { get; set; }
	public int pondRes { get; set; }
	public int caveRes { get; set; }
	
	// wooly beans?
	public int woolyBeans { get; set; }

	/**
	 * Produces a singleton on awake
	 */
	override public void Start() {
		
		if (state == null) {
			DontDestroyOnLoad(gameObject);
			state = this;
		} else if (state != this) {
			Destroy(gameObject);
		}

		state.buildings = new Dictionary<Tuple, Building>();

	/*	
		// set the fields until we can load
		state.fire = 0;

		fireEle = 2;
		earthEle = 2;
		waterEle = 2;
		airEle = 2;
		poofCount = 3;
<<<<<<< HEAD
        */
		
		woolyBeans = 0;
	}

	
	/**
	 * Saves all relevant data to a local file
	 * NOTE: This does not perform a write back but
	 * rather creates a fresh file every time.
	 */
	public void Save() {
		
		// Dumps JSON to text file
		System.IO.File.WriteAllText(Application.persistentDataPath + "/save_state.dat", this.jsonify());
	}
	
	/**
	 * Loads all relevant data from a file
	 */
	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/save_state.dat")) {
			string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/save_state.dat");
			
			//PlayerData data = JSON.Deserialize<PlayerData>(json);
			// Reassign all variables here
			// ie: this.setHealth(data.getHealth());
			//GetPlayerData(data);
		}
	}

	/**
	 * Pushes player data to server
	 */
	public void PushToServer() {
		
		Debug.Log ("data.ToJSON");
		string buildingJSON = this.jsonify();
		
		
		// TODO Send JSON to server
        StartCoroutine(GetHTTP.toSave(buildingJSON));
	}
	
	// turns the save data into a JSON String
	public String jsonify() {
		
		String jsonPlayerData = "{ ";
		
		jsonPlayerData += "\"name\": \"" + "timothy" + "\", ";
		jsonPlayerData += "\"email\": \"" + "timothy@yo.com" + "\", ";
		jsonPlayerData += "\"level\": \"" + "1" + "\", ";
		jsonPlayerData += "\"user_id\": \"" + userID  + "\", ";
		jsonPlayerData += "\"username\": \"" + username + "\", ";
		jsonPlayerData += "\"password\": \"" + userPassword + "\", ";
		jsonPlayerData += "\"experience\": \"" + userExperience + "\", ";
		jsonPlayerData += "\"hq_level\": \"" + hqLevel  + "\", ";
		jsonPlayerData += "\"fire\": \"" + fire + "\", ";
		jsonPlayerData += "\"air\": \"" + air + "\", ";
		jsonPlayerData += "\"water\": \"" + water + "\", ";
		jsonPlayerData += "\"earth\": \"" + earth + "\", ";
		jsonPlayerData += "\"maxFire\": \"" + maxFire + "\", ";
		jsonPlayerData += "\"maxWater\": \"" + maxWater + "\", ";
		jsonPlayerData += "\"maxAir\": \"" + maxAir + "\", ";
		jsonPlayerData += "\"maxEarth\": \"" + maxEarth + "\", ";
		jsonPlayerData += "\"fireElements\": \"" + fireEle + "\", ";
		jsonPlayerData += "\"waterElements\": \"" + waterEle + "\", ";
		jsonPlayerData += "\"earthElements\": \"" + earthEle + "\", ";
		jsonPlayerData += "\"airElements\": \"" + airEle + "\", ";
		jsonPlayerData += "\"resource_buildings\": [ ";
		
		foreach ( KeyValuePair<Tuple, Building> entry in buildings) {
			jsonPlayerData += "{ ";
			jsonPlayerData += "\"id\": \"" + entry.Value.ID + "\", ";
			jsonPlayerData += "\"x_coordinate\": \"" + entry.Key.x + "\", ";
			jsonPlayerData += "\"y_coordinate\": \"" + entry.Key.y + "\", ";
			jsonPlayerData += "\"size\": \"" + entry.Value.size + "\", ";
            jsonPlayerData += "\"new\": \"" + entry.Value.created + "\" ";
			jsonPlayerData += "},";
		}
		
		jsonPlayerData = jsonPlayerData.TrimEnd (',');
		
		jsonPlayerData += "], ";
		jsonPlayerData += "\"decorative_buildings\": []";
		jsonPlayerData += "}";
		
		
		return jsonPlayerData;
		
	}

	// This method populates the save data with data from a json string
	public void loadJSON(String json){

		JSONArray loadedResourceBuildings;
		JSONArray loadedDecorativeBuildings;

		JSONNode data = JSON.Parse (json);

		userID = data ["user_id"].AsInt;
		userLevel = data ["level"].AsInt;
		userExperience = data ["experience"].AsInt;
        userPassword = data ["password"];
        username = data ["username"];
		hqLevel = data ["headquarters_level"].AsInt;
		fire = data ["fire"].AsInt;
		water = data ["water"].AsInt;
		earth = data ["earth"].AsInt;
		air = data ["air"].AsInt;
		maxFire = data ["max_fire"].AsInt;
		maxWater = data ["max_water"].AsInt;
		maxEarth = data ["max_earth"].AsInt;
		maxAir = data ["max_air"].AsInt;
		fireEle = data ["fire_ele"].AsInt;
		waterEle = data ["water_ele"].AsInt;
		earthEle = data ["earth_ele"].AsInt;
		airEle = data ["air_ele"].AsInt;
		poofCount = data ["poof_count"].AsInt;

		// load the buildings
		loadedResourceBuildings = data ["resource_buildings"].AsArray;
		loadedDecorativeBuildings = data ["decorative_buildings"].AsArray;

		foreach (JSONNode building in loadedResourceBuildings) {
			int x = building["position_x"].AsInt;
			int y = building["position_y"].AsInt;

            // Retrieves a building from the resource buildings list
            Debug.Log("index: " + building["building_info_id"].AsInt);
            if (PrefabManager.prefabManager == null) {
                Debug.Log("prefab manager");
            }
			Building newBuilding = PrefabManager.prefabManager.resourceBuildings[building["building_info_id"].AsInt];

			buildings.Add(new Tuple(x, y), newBuilding);
		}
		//TODO foreach loop for decorative building
		foreach (JSONNode building in loadedDecorativeBuildings) {
		}
	}
}
