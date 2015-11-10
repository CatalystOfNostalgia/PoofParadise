using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
//currently no player game object, could be slider...

// a class to deal with resources
public class ResourceIncrementer : GamePanel
{
	// singleton object
	public static ResourceIncrementer incrementer;

    private Slider[] sliders;
	
	override public void Start() {
		// create the singleton
		if (incrementer == null) {
			DontDestroyOnLoad(gameObject);
			incrementer = this;
		} else if (incrementer != this) {
			Destroy(gameObject);
		}

        sliders = RetrieveSliderList("Sliders");
	}

    public override void GeneratePanel()
    {
        throw new NotImplementedException();
    }

    /**
     * Retrieves slider by name
     */
    private Slider GetSliderByName(string name)
    {
        foreach (Slider s in sliders)
        {
            if (s.name == name)
            {
                return s;
            }
        }
        return null;
    }


    /**
     * A helper method for handling various types of resources
     */
/**
    private void ManageSlider(int add, ref int current, int max, Slider s)
    {
        Debug.Log(SaveState.state.fire);

        // If the slider is not filled yet
        if (current <= max)
        {
            // increment element
            current = current + add;

            // Reassign slider
            s.value = current;
        }

        // Otherwise, hold the state
        else
        {
            current = max;
        }
    }
    
    //copypasta code for now. make it more modular later
    public void ResourceGain (int amount, ResourceBuilding.ResourceType type) {
        int dummy;
        switch (type) {
            case ResourceBuilding.ResourceType.fire:
                dummy = SaveState.state.fire;
                ManageSlider(amount, ref dummy, SaveState.state.maxFire, GetSliderByName("Fire Slider"));
                SaveState.state.fire = dummy;
                break;
		    case ResourceBuilding.ResourceType.water:
                dummy = SaveState.state.water;
                ManageSlider(amount, ref dummy, SaveState.state.maxWater, GetSliderByName("Water Slider"));
                SaveState.state.water = dummy;
			    break;
		    case ResourceBuilding.ResourceType.air:
                dummy = SaveState.state.air;
                ManageSlider(amount, ref dummy, SaveState.state.maxAir, GetSliderByName("Air Slider"));
                SaveState.state.air = dummy;
			    break;
		    case ResourceBuilding.ResourceType.earth:
                dummy = SaveState.state.earth;
                ManageSlider(amount, ref dummy, SaveState.state.maxEarth, GetSliderByName("Earth Slider"));
                SaveState.state.earth = dummy;
			    break;
		    default:
			    Debug.Log("ResourceIncrementer: Illegal resource type");
			    break;
		}

	}
*/
}
