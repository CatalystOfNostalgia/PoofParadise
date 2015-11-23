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
	public int userID { get; set; }
	public int userLevel { get; set; }
	public int userExperience { get; set; }
	public int hqLevel { get; set; }
	public int poofCount { get; set; }
	// List game state variables here

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

	// buildings
	public Dictionary<Tuple, Building> resourceBuildings { get; set; }
	public Dictionary<Tuple, Building> decorativeBuildings { get; set; }


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
		
		// set the fields until we can load
		state.fire = 0;
		state.resourceBuildings = new Dictionary<Tuple, Building>();

		fireEle = 2;
		earthEle = 2;
		waterEle = 2;
		airEle = 2;
		poofCount = 3;

		firetreeRes = 0;
		windmillRes = 0;
		pondRes = 0;
		caveRes = 0;
		
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
	}
	
	/**
	 * Pulls player data from server
	 */
	public void PullFromServer() {
		
		// get the JSON from the server
		String userInfo = GetHTTP.login("ted1", "password");

		loadJSON (userInfo);
		
	}

	// turns the save data into a JSON String
	public String jsonify() {
		
		String jsonPlayerData = "{ ";
		
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
		
		foreach ( KeyValuePair<Tuple, Building> entry in resourceBuildings) {
			jsonPlayerData += "{ ";
			jsonPlayerData += "\"x_coordinate\": \"" + entry.Key.x + "\", ";
			jsonPlayerData += "\"y_coordinate\": \"" + entry.Key.y + "\", ";
			jsonPlayerData += "\"size\": \"" + entry.Value.size + "\" ";
			jsonPlayerData += "},";
		}
		
		jsonPlayerData = jsonPlayerData.TrimEnd (',');
		
		jsonPlayerData += "], ";
		jsonPlayerData += "\"decorative_buildings\": []";
		jsonPlayerData += "}";
		
		
		return jsonPlayerData;
		
	}

	// This method populates the save data with data from a json string
	private void loadJSON(String json){

		JSONArray loadedResourceBuildings;
		JSONArray loadedDecorativeBuildings;

		JSONNode data = JSON.Parse (json);

		userID = data ["user_id"].AsInt;
		userLevel = data ["level"].AsInt;
		userExperience = data ["experience"].AsInt;
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
			Building newBuilding = PrefabManager.prefabManager.resourceBuildings[building["building_info_id"].AsInt];

			resourceBuildings.Add(new Tuple(x, y), newBuilding);
		}

	}
}
