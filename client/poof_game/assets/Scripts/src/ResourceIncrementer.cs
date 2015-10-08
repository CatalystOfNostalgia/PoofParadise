using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//currently no player game object, could be slider...

// a class to deal with resources
public class ResourceIncrementer : MonoBehaviour
{
	// singleton object
	public static ResourceIncrementer incrementer;
	// reference to the slider
	public Slider goldSlider; 

	
	void Awake () {

		//create the singleton
		if (incrementer == null) {
			DontDestroyOnLoad(gameObject);
			incrementer = this;
		} else if (incrementer != this) {
			Destroy(gameObject);
		}
	}

	public void ResourceGain (int amount) {

		// if we haven't filled the slider
		if (SaveState.state.gold <= 100) {

			// increment the gold
			SaveState.state.gold = SaveState.state.gold + amount;


			Debug.Log ("incremented : " + SaveState.state.gold);

			// Set the health bar's value to the current health.
			goldSlider.value = SaveState.state.gold;

			Debug.Log ("incremented : " + goldSlider.value);


		} else {
			SaveState.state.gold = 100;
		}
	} 
}
