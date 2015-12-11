using UnityEngine.UI;
using System;
using UnityEngine;

/**
 * A class to deal with resources
 */
public class ResourceIncrementer : GamePanel
{
	// Singleton object
	public static ResourceIncrementer incrementer;

    // List of interactive objects on this panel
    private Slider[] sliders;
    private Text[] counters;
	
    /**
     * Overrides the start function provided by GamePanel
     */
	override public void Start() {

		// Create the singleton
		if (incrementer == null) {
			DontDestroyOnLoad(gameObject);
			incrementer = this;
		} else if (incrementer != this) {
			Destroy(gameObject);
		}

        sliders = RetrieveSliderList("Sliders");
        counters = GetComponentsInChildren<Text>();

        Debug.Log("fire: " + SaveState.state.fire + " water: " + SaveState.state.water);
        InitializeSliders();
	}

    /**
     * Overrides the Generate Panel function provided
     * by GamePanel
     */
    public override void GeneratePanel()
    {
        Debug.LogError ("DONT GENERATE THE RESOURCE INCREMENTER!!!!");
    }

    /**
     * Initialzes the sliders on the panel
     */
    public void InitializeSliders()
    {

        Debug.Log("initializing resource incrementor");

        GetSliderByName("Fire Slider").maxValue = 200;
        GetSliderByName("Water Slider").maxValue = 200;
        GetSliderByName("Earth Slider").maxValue = 200;
        GetSliderByName("Wind Slider").maxValue = 200;

        GetSliderByName("Fire Slider").value = SaveState.state.fire;
        GetSliderByName("Water Slider").value = SaveState.state.water;
        GetSliderByName("Earth Slider").value = SaveState.state.earth;
        GetSliderByName("Wind Slider").value = SaveState.state.air;

        GetTextByName("FireCount").text = "" + SaveState.state.fire;
        GetTextByName("WaterCount").text = "" + SaveState.state.fire;
        GetTextByName("EarthCount").text = "" + SaveState.state.earth;
        GetTextByName("AirCount").text = "" + SaveState.state.air;
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
     * Retrieves text field by name
     */
    private Text GetTextByName(string name) {
        foreach (Text t in counters)
        {
            if (t.name == name)
            {
                return t;
            }
        }
        return null;
    }

    /**
     * A helper method for handling various types of resources
     */
    private bool ManageSlider(int add, ref int current, int max, Slider s)
    {
    	s.maxValue = max;
        // If the slider is not filled yet
        if (current <= max && current + add >= 0) // current + add makes sure the purchase is legal
        {
            // increment element
            current = current + add;

            // Reassign slider
            s.value = current;
            return true;
        }
        else if (current + add < 0)
        {
            // Do nothing
            return false;
        }

        // Otherwise, hold the state
        else
        {
            current = max;
            return true;
        }
    }
    
    /**
     * Copypasta code for now. make it more modular later
     */
    public bool ResourceGain (int amount, ResourceBuilding.ResourceType type) {
        int dummy;
        bool pay;
        switch (type) {
            case ResourceBuilding.ResourceType.fire:
                dummy = SaveState.state.fire;
                pay = ManageSlider(amount, ref dummy, SaveState.state.maxFire, GetSliderByName("Fire Slider"));
                GetTextByName("FireCount").text = "" + dummy;
                SaveState.state.fire = dummy;
                break;
		    case ResourceBuilding.ResourceType.water:
                dummy = SaveState.state.water;
                pay = ManageSlider(amount, ref dummy, SaveState.state.maxWater, GetSliderByName("Water Slider"));
                GetTextByName("WaterCount").text = "" + dummy;
                SaveState.state.water = dummy;
			    break;
		    case ResourceBuilding.ResourceType.air:
                dummy = SaveState.state.air;
                pay = ManageSlider(amount, ref dummy, SaveState.state.maxAir, GetSliderByName("Wind Slider"));
                GetTextByName("AirCount").text = "" + dummy;
                SaveState.state.air = dummy;
			    break;
		    case ResourceBuilding.ResourceType.earth:
                dummy = SaveState.state.earth;
                pay = ManageSlider(amount, ref dummy, SaveState.state.maxEarth, GetSliderByName("Earth Slider"));
                GetTextByName("EarthCount").text = "" + dummy;
                SaveState.state.earth = dummy;
			    break;
		    default:
			    Debug.LogError("ResourceIncrementer: Illegal resource type");
                pay = false;
			    break;
		}
        return pay;

	}

}
