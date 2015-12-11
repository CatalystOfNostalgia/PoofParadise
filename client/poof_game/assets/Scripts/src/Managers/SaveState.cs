using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;

/**
 * An object which oversees the state of the entire game
 * The server interfaces with this object directly
 */
public class SaveState : Manager {
            
	// Allows the scene to access this object without searching for it
	public static SaveState state;

	// Player data
    public string username { get; set; }
    public string userPassword { get; set; }
	public int userID { get; set; }
	public int userLevel { get; set; }
	public int userExperience { get; set; }
	public int hqLevel { get; set; }
	public int poofCount { get; set; }
    public int poofLimit { get; set; }

	// Resources
	public int fire { get; set; }
	public int air { get; set; }
	public int water { get; set; }
	public int earth { get; set; }
	public int maxFire { get; set; }
	public int maxAir { get; set; }
	public int maxWater { get; set; }
	public int maxEarth { get; set; }

	// Elemari
	public int fireEle { get; set; }
	public int waterEle { get; set; }
	public int earthEle { get; set; }
	public int airEle { get; set; }

	// buildings
	public Dictionary<Tuple, ResourceBuilding> resourceBuildings { get; set; }
	public Dictionary<Tuple, DecorativeBuilding> decorativeBuildings { get; set; }
	public Dictionary<Tuple, ResidenceBuilding> residenceBuildings { get; set; }
    public HeadQuarterBuilding hq { get; set; }
    public Tuple hqLocation { get; set; }

    public BuildingInformationManager buildingInformationManager;

	//resource collection fields // currently unused
	public int firetreeRes { get; set; }
	public int windmillRes { get; set; }
	public int pondRes { get; set; }
	public int caveRes { get; set; }
	
	// The monotized in-game resource
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

		state.resourceBuildings = new Dictionary<Tuple, ResourceBuilding>();
		state.decorativeBuildings = new Dictionary<Tuple, DecorativeBuilding>();
		state.residenceBuildings = new Dictionary<Tuple, ResidenceBuilding>();
		
        buildingInformationManager = new BuildingInformationManager();
		woolyBeans = 0;
        poofLimit = 0;
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
		string buildingJSON = this.jsonify();
		// TODO Send JSON to server
        StartCoroutine(GetHTTP.toSave(buildingJSON, updateBuildings));
	}

    // updates the saved building with their new IDs
    private void updateBuildings(string response) {

        if (response.Length > 0) {

            Debug.Log(response);

            JSONNode data = JSON.Parse(response);

            int i = 0;
            JSONArray resourceIDs = data["building_ids"]["resource_buildings"].AsArray;
            JSONArray decorativeIDs = data["building_ids"]["decorative_buildings"].AsArray;

            foreach (KeyValuePair<Tuple, ResourceBuilding> b in resourceBuildings) {

                if (b.Value.created) {
                    b.Value.ID = resourceIDs[i].AsInt; 
                    b.Value.created = false;
                    i++;
                }
            }

            i = 0;

            foreach (KeyValuePair<Tuple, DecorativeBuilding> b in decorativeBuildings) {

                if (b.Value.created) {
                    b.Value.ID = decorativeIDs[i].AsInt;
                    b.Value.created = false;
                    i++;
                }
            }
        }
    }

    // adds a building to the appropriate dictionary
    public void addBuilding(Tuple t, Building b) {
        
        if (b.GetType() == typeof(DecorativeBuilding)) {
            DecorativeBuilding decBuilding = (DecorativeBuilding)b;
            SaveState.state.decorativeBuildings.Add (t, decBuilding);

        } else if (b.GetType() == typeof(ResourceBuilding)) {
            ResourceBuilding resBuilding = (ResourceBuilding)b;
            SaveState.state.resourceBuildings.Add (t, resBuilding);

        } else if (b.GetType() == typeof(ResidenceBuilding)) {
            ResidenceBuilding resBuilding = (ResidenceBuilding)b;
            SaveState.state.residenceBuildings.Add (t, resBuilding);
        }

    }

    // removes a building from the dicts
    public bool removeBuilding(Tuple t) {

        if (decorativeBuildings.Remove(t) || 
            resourceBuildings.Remove(t) ||
            residenceBuildings.Remove(t)) {

            return true;
        }

        return false;

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

        jsonPlayerData += "\"headquarters_level\": \"" + hqLevel + "\", ";
        jsonPlayerData += "\"hq_pos_x\": \"" + hqLocation.x + "\", ";
        jsonPlayerData += "\"hq_pos_y\": \"" + hqLocation.y + "\", ";
        jsonPlayerData += "\"resource_buildings\": [ ";
		
		foreach ( KeyValuePair<Tuple, ResourceBuilding> entry in resourceBuildings) {
			jsonPlayerData += "{ ";
            jsonPlayerData += "\"building_info_id\": \"" + entry.Value.buildingInfoID + "\", ";
            jsonPlayerData += "\"level\": \"" + entry.Value.level + "\", ";
			jsonPlayerData += "\"id\": \"" + entry.Value.ID + "\", ";
			jsonPlayerData += "\"position_x\": \"" + entry.Key.x + "\", ";
			jsonPlayerData += "\"position_y\": \"" + entry.Key.y + "\", ";
			jsonPlayerData += "\"size\": \"" + entry.Value.size + "\", ";
            jsonPlayerData += "\"new\": \"" + entry.Value.created + "\" ";
			jsonPlayerData += "},";
		}

		
		jsonPlayerData = jsonPlayerData.TrimEnd (',');
		jsonPlayerData += "], ";

		jsonPlayerData += "\"decorative_buildings\": [";
		foreach ( KeyValuePair<Tuple, DecorativeBuilding> entry in decorativeBuildings) {

			jsonPlayerData += "{ ";
            jsonPlayerData += "\"building_info_id\": \"" + entry.Value.buildingInfoID + "\", ";
            jsonPlayerData += "\"level\": \"" + entry.Value.level + "\", ";
			jsonPlayerData += "\"id\": \"" + entry.Value.ID + "\", ";
			jsonPlayerData += "\"position_x\": \"" + entry.Key.x + "\", ";
			jsonPlayerData += "\"position_y\": \"" + entry.Key.y + "\", ";
			jsonPlayerData += "\"size\": \"" + entry.Value.size + "\", ";
            jsonPlayerData += "\"new\": \"" + entry.Value.created + "\" ";
			jsonPlayerData += "},";
		}

		jsonPlayerData = jsonPlayerData.TrimEnd (',');
        jsonPlayerData += "]";

		jsonPlayerData += "}";

        Debug.Log(jsonPlayerData);

		return jsonPlayerData;
		
	}

	// This method populates the save data with data from a json string
	public void loadJSON(String json){

        Debug.Log(json);

		JSONArray loadedResourceBuildings;
		JSONArray loadedDecorativeBuildings;

		JSONNode data = JSON.Parse (json);

		userID = data ["user_id"].AsInt;
		userLevel = data ["level"].AsInt;
		userExperience = data ["experience"].AsInt;
        userPassword = data ["password"];
        username = data ["username"];
		hqLevel = data ["headquarters_level"].AsInt;
		//change this back 
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
            if (PrefabManager.prefabManager == null) {
                Debug.LogError("[SaveState] Prefab manager is null");
            }

            ResourceBuilding newBuilding = PrefabManager.prefabManager.getResourceBuilding(building["building_info_id"].AsInt);

            newBuilding.created = false;
            newBuilding.ID = building["id"].AsInt;
            newBuilding.buildingInfoID = building["building_info_id"].AsInt;

			resourceBuildings.Add(new Tuple(x, y), newBuilding);
		}

		foreach (JSONNode building in loadedDecorativeBuildings) {
			int x = building["position_x"].AsInt;
			int y = building["position_y"].AsInt;

            // Retrieves a building from the resource buildings list
            Debug.Log("index: " + building["building_info_id"].AsInt);
            if (PrefabManager.prefabManager == null) {
                Debug.Log("prefab manager");
            }

            DecorativeBuilding newBuilding = PrefabManager.prefabManager.getDecorativeBuilding(building["building_info_id"].AsInt);

            Debug.Log(newBuilding.name + " has info id of: " + building["building_info_id"].AsInt);

            newBuilding.created = false;
            newBuilding.ID = building["id"].AsInt;
            newBuilding.buildingInfoID = building["building_info_id"].AsInt;

			decorativeBuildings.Add(new Tuple(x, y), newBuilding);
		}

        hqLocation = new Tuple(data["hq_pos_x"].AsInt, data["hq_pos_y"].AsInt);
        //since array start at 0, lv 1-> index 0, lv 2 -> index 1
        hq = PrefabManager.prefabManager.headQuarterBuildings[hqLevel-1];

	}
}
