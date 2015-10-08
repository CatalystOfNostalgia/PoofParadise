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

	public void ResourceGain (int amount, ResourceBuilding.ResourceType type) {

		/*
		using fire = ResourceBuilding.ResourceType.fire;

		switch (type) {

			case fire:

		}

*/

		// if we haven't filled the slider
		if (SaveState.state.fire <= 100) {

			// increment the gold
			SaveState.state.fire = SaveState.state.fire + amount;


			Debug.Log ("incremented : " + SaveState.state.fire);

			// Set the health bar's value to the current health.
			goldSlider.value = SaveState.state.fire;

			Debug.Log ("incremented : " + goldSlider.value);


		} else {
			SaveState.state.fire = 100;
		}
	} 
}
