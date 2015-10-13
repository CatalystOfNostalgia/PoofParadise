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
	public Slider fireSlider; 
	public Slider waterSlider; 
	public Slider airSlider; 
	public Slider earthSlider; 

	
	void Awake () {

		//create the singleton
		if (incrementer == null) {
			DontDestroyOnLoad(gameObject);
			incrementer = this;
		} else if (incrementer != this) {
			Destroy(gameObject);
		}
	}

	//copypasta code for now. make it more modular later
	public void ResourceGain (int amount, ResourceBuilding.ResourceType type) {

		switch (type) {
		case ResourceBuilding.ResourceType.fire:
			// if we haven't filled the slider
			if (SaveState.state.fire <= 100) {
				
				// increment the gold
				SaveState.state.fire = SaveState.state.fire + amount;
				
				
				//Debug.Log ("incremented : " + SaveState.state.fire);
				
				// Set the health bar's value to the current health.
				fireSlider.value = SaveState.state.fire;
				
				//Debug.Log ("incremented : " + goldSlider.value);
				
				
			} else {
				SaveState.state.fire = 100;
			}
			break;
		case ResourceBuilding.ResourceType.water:
			// if we haven't filled the slider
			if (SaveState.state.water <= 100) {
				
				// increment the gold
				SaveState.state.water = SaveState.state.water + amount;
				
				
				//Debug.Log ("incremented : " + SaveState.state.fire);
				
				// Set the health bar's value to the current health.
				waterSlider.value = SaveState.state.water;
				
				//Debug.Log ("incremented : " + goldSlider.value);
				
				
			} else {
				SaveState.state.water = 100;
			}
			break;
		case ResourceBuilding.ResourceType.air:
			// if we haven't filled the slider
			if (SaveState.state.air <= 100) {
				
				// increment the gold
				SaveState.state.air = SaveState.state.air + amount;
				
				
				//Debug.Log ("incremented : " + SaveState.state.fire);
				
				// Set the health bar's value to the current health.
				airSlider.value = SaveState.state.air;
				
				//Debug.Log ("incremented : " + goldSlider.value);
				
				
			} else {
				SaveState.state.air = 100;
			}
			break;
		case ResourceBuilding.ResourceType.earth:
			// if we haven't filled the slider
			if (SaveState.state.earth <= 100) {
				
				// increment the gold
				SaveState.state.earth = SaveState.state.earth + amount;
				
				
				//Debug.Log ("incremented : " + SaveState.state.fire);
				
				// Set the health bar's value to the current health.
				earthSlider.value = SaveState.state.earth;
				
				//Debug.Log ("incremented : " + goldSlider.value);
				
				
			} else {
				SaveState.state.earth = 100;
			}
			break;
		default:
			Debug.Log("ResourceIncrementer: Illegal resource type");
			break;
		}

	}
}
