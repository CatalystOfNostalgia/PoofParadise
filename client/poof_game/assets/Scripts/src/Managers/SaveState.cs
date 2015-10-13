using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using SimpleJSON;

public class SaveState : MonoBehaviour {

	// Allows the scene to access this object without searching for it
	public static SaveState state;
	// List game state variables here
	// Format: public <Type> <Name> { get; set; }
	public int fire { get; set; }
	public int air { get; set; }
	public int water { get; set; }
	public int earth { get; set; }
	public int maxFire { get; set; }
	public int maxAir { get; set; }
	public int maxWater { get; set; }
	public int maxEarth { get; set; }
    public int fireEle { get; set; }
    public int waterEle { get; set; }
    public int earthEle { get; set; }
    public int airEle { get; set; }
    public Dictionary<Tuple, Building> existingBuildingDict { get; set; }


    /**
	 * A helper method for passing data from this
	 * game state to the serializable object
	 */
    private void SetPlayerData(PlayerData pd) {
		pd.air = this.air;
		pd.fire = this.fire;
		pd.water = this.water;
		pd.earth = this.earth;
        pd.maxFire = this.maxFire;
		pd.maxAir = this.maxAir;
		pd.maxWater = this.maxWater;
		pd.maxEarth = this.maxEarth;
        pd.fireEle = this.fireEle;
        pd.waterEle = this.fireEle;
        pd.earthEle = this.earthEle;
        pd.airEle = this.airEle;
        pd.existingBuildingDict = this.existingBuildingDict;
	}

	/**
	 * A helper method for pulling data from 
	 * the serializable object to this game state
	 */
	private void GetPlayerData(PlayerData pd) {
		this.air = pd.air;
		this.fire = pd.fire;
		this.water = pd.water;
		this.earth = pd.earth;
		this.maxFire = pd.maxFire;
		this.maxAir = pd.maxAir;
		this.maxWater = pd.maxWater;
		this.maxEarth = pd.maxEarth;
        this.fireEle = pd.fireEle;
        this.waterEle = pd.waterEle;
        this.earthEle = pd.earthEle;
        this.airEle = pd.airEle;
        this.existingBuildingDict = pd.existingBuildingDict;
	}

	/**
	 * Produces a singleton on awake
	 */
	public void Awake() {

		if (state == null) {
			DontDestroyOnLoad(gameObject);
			state = this;
		} else if (state != this) {
			Destroy(gameObject);
		}

		// set the fields until we can load
		state.fire = 0;
		state.existingBuildingDict = new Dictionary<Tuple, Building>();
        fireEle = 1;
        earthEle = 1;
        waterEle = 1;
        airEle = 1;
	}

	/**
	 * Pushes player data to server
	 */
	public void PushToServer() {

		PlayerData data = new PlayerData ();
		SetPlayerData (data);
        
		Debug.Log ("data.ToJSON");
		string buildingJSON = data.jsonify();

		Debug.Log (buildingJSON);

		data = new PlayerData (buildingJSON);

		Debug.Log (data.air);

		
		// TODO Send JSON to server
	}

	/**
	 * Pulls player data from server
	 */
	public void PullFromServer() {
		// TODO Get JSON from server
		// GetPlayerData (data);
	}

	/**
	 * Saves all relevant data to a local file
	 * NOTE: This does not perform a write back but
	 * rather creates a fresh file every time.
	 */
	public void Save() {
		PlayerData data = new PlayerData();
		// Insert data from controller to data object
		// ie: data.setExp(this.getExp());
		SetPlayerData (data);

        // Dumps JSON to text file
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save_state.dat", data.jsonify());
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
}

[Serializable]
class PlayerData {
	
	/**
	 * Place the variables you want to save in this section
	 * Format: public <Type> <Name> { get; set; }
	 */
	public int fire { get; set; }
	public int air { get; set; }
	public int water { get; set; }
	public int earth { get; set; }
	public int maxFire { get; set; }
	public int maxAir { get; set; }
	public int maxWater { get; set; }
	public int maxEarth { get; set; }
	public int fireEle { get; set; }
	public int waterEle { get; set; }
	public int earthEle { get; set; }
	public int airEle { get; set; }
	public Dictionary<Tuple, Building> existingBuildingDict { get; set; }

	// constructs save data from a JSON string
	public PlayerData(String jsonData) {

		JSONNode data = JSON.Parse (jsonData);
		
		this.fire = data ["fire"].AsInt;
		this.air = data ["air"].AsInt;
		this.water = data ["water"].AsInt;
		this.earth = data ["earth"].AsInt;
		this.maxFire = data ["maxFire"].AsInt;
		this.maxAir = data ["maxAir"].AsInt;
		this.maxEarth = data ["maxEarth"].AsInt;
		this.maxWater = data ["maxWater"].AsInt;
		this.fireEle = data ["fireEle"].AsInt;
		this.airEle = data ["airEle"].AsInt;
		this.earthEle = data ["earthEle"].AsInt;
		this.waterEle = data ["waterEle"].AsInt;

		// need to figure out typecast
		// this.existingBuildingDict = data ["resourceBuildings"].AsArray.;

	}

	// an empty constructor to appease the compiler, should be removed and all calls
	// should go through the json constructor.
	public PlayerData(){

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

		foreach ( KeyValuePair<Tuple, Building> entry in existingBuildingDict) {
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
	
}
