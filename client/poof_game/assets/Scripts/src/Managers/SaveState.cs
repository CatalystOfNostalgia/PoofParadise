using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SaveState : MonoBehaviour {

	// Allows the scene to access this object without searching for it
	public static SaveState state;

	// player data from the server
	public String data { get; set; }

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

	/**
	 * Produces a singleton on awake
	 */
	public void Awake() {

		Debug.Log ("waking");

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

		state.PullFromServer ();

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

		Debug.Log (userInfo);

		// TODO parse the JSON into data

		// TODO build the grid from the data

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
}
