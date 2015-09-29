using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class SaveState : MonoBehaviour {

	// Allows the scene to access this object without searching for it
	public static SaveState state;

	// List game state variables here

	/**
	 * Produces a singleton on awake
	 */
	private void Awake() {
		if (state == null) {
			DontDestroyOnLoad(gameObject);
			state = this;
		} else if (state != this) {
			Destroy(gameObject);
		}
	}

	/**
	 * Saves all relevant data to a local file
	 * NOTE: This does not perform a write back but
	 * rather creates a fresh file every time.
	 */
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/save_state.dat");
		
		PlayerData data = new PlayerData();
		// Insert data from controller to data object
		// ie: data.setExp(this.getExp());
		
		bf.Serialize(file, data);
		file.Close();
	}

	/**
	 * Loads all relevant data from a file
	 */
	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/save_state.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/save_state.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close ();
			
			// Reassign all variables here
			// ie: this.setHealth(data.getHealth());

		}
	}
}

[Serializable]
class PlayerData {
	
	/**
	 * Place the variables you want to save in this section
	 * Also be sure to write getter/setter methods
	 */
	

}
