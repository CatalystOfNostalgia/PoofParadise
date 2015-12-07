using UnityEngine;
using System.Collections;

public class ResourceManager : Manager {
	
	// Static reference to this script to make it accessible from anywhere
	public static ResourceManager rm;
	
	// Reference to the buildings within the scene because fuck doing this right
	private GameObject buildings;
	
	// physical amount that resource collection buildings create on a per tick basis
	//		used as a base for resource gain calculations; modify this to speed up or slow down the process overall
	public int rTick = 5;
	
	// delay number for resource collection; lower number => faster collection
	public float tickDelay = 10f;
	
	// base for resource cap D:
	public int resourceCapBase = 100;
	
	/* modifiers for the different levels of resource collection buildings,
			higher level buildings collect more resources
			index 0: unupgraded buildings modifier
			index 1: level 2's modifier
			etc...
	*/
	public int[] levelModifiers = {1, 2, 3}; // assuming there's only 3 levels of buildings
	
	// Initializes the script as a singleton object
	override public void Start() {
		
		if (rm == null) {
			DontDestroyOnLoad(gameObject);
			rm = this;
		} else if (rm != this) {
			Destroy(gameObject);
		}
		this.InvokeRepeating("resourceTick", 5f, tickDelay);
		buildings = GameObject.Find("Buildings");
	}
	
	/* Retrieves the resource values from SaveState and pushes them into an array
		Index 0: Fire
		Index 1: Air
		Index 2: Water
		Index 3: Earth
	*/
	public int[] getResourceAmounts () {
		int[] toReturn = new int[4];
		toReturn[0] = SaveState.state.fire;
		toReturn[1] = SaveState.state.air;
		toReturn[2] = SaveState.state.water;
		toReturn[3] = SaveState.state.earth;
		return toReturn;
	}
	
	/* Spends a certain amount of a given resource, return value determines if subtraction was successful
			type refers to which type of resource you are spending
			amount obviously refers to the amount you are withdrawing
	*/
	public bool spendResource (ResourceBuilding.ResourceType type, int amount) {
		switch(type) {
			case ResourceBuilding.ResourceType.fire:
				if (amount > SaveState.state.fire) 
					return false;
				else {
					SaveState.state.fire -= amount;
					return true;
				}
			case ResourceBuilding.ResourceType.air:
				if (amount > SaveState.state.air) 
					return false;
				else {
					SaveState.state.air -= amount;
					return true;
				}
			case ResourceBuilding.ResourceType.water:
				if (amount > SaveState.state.water) 
					return false;
				else {
					SaveState.state.water -= amount;
					return true;
				}
			case ResourceBuilding.ResourceType.earth:
				if (amount > SaveState.state.earth) 
					return false;
				else {
					SaveState.state.earth -= amount;
					return true;
				}
		default:
				Debug.Log("ResourceManager => spendResource: Illegal resource type");
				return false;
		}
	}
	
	/* Updates the resource bar with new amount of resources on a given clock tick
			resources that are gained are based on the current level and amount of resource buildings you own
			max resource cap determined by SaveState
	*/
	public void resourceTick () {
		checkResourceCap();
		int[] resourceGains = getResourceGains();
		Debug.Log (resourceGains[0] + ", " + resourceGains[1] + ", " + resourceGains[2] + ", " + resourceGains[3]);
		ResourceIncrementer.incrementer.ResourceGain(resourceGains[0], ResourceBuilding.ResourceType.fire);
		ResourceIncrementer.incrementer.ResourceGain(resourceGains[1], ResourceBuilding.ResourceType.air);
		ResourceIncrementer.incrementer.ResourceGain(resourceGains[2], ResourceBuilding.ResourceType.water);
		ResourceIncrementer.incrementer.ResourceGain(resourceGains[3], ResourceBuilding.ResourceType.earth);
		
	}
	
	// Hard coded check to make sure the resource cap is consistently up to date
	// 	Cap is based on the hq level, currently the base * level
	private void checkResourceCap () {
		SaveState.state.maxFire = resourceCapBase * (SaveState.state.hqLevel + 1);
		SaveState.state.maxAir = resourceCapBase * (SaveState.state.hqLevel + 1);
		SaveState.state.maxWater = resourceCapBase * (SaveState.state.hqLevel + 1);
		SaveState.state.maxEarth = resourceCapBase * (SaveState.state.hqLevel + 1);
	}
	
	// Consults SaveState to retrieve how many of each resource building there is and of what level,
	//		to determine how much resources are gathered.
	private int[] getResourceGains () {
		int[] toReturn = new int[4];
		for (int i = 0; i < buildings.transform.childCount; i++) {
			string n = buildings.transform.GetChild(i).name;
			if (n.Contains("Cave")) {
				toReturn[3] += rTick;
				if (n.Contains("Lvl 2"))
					toReturn[3] += rTick;
			}
			if (n.Contains("Fire")) {
				toReturn[0] += rTick;
				if (n.Contains("Lvl 2"))
					toReturn[0] += rTick;
			}
			if (n.Contains("Windmill")) {
				toReturn[1] += rTick;
				if (n.Contains("Lvl 2"))
					toReturn[1] += rTick;
			}
			if (n.Contains("Pond")) {
				toReturn[2] += rTick;
				if (n.Contains("Lvl 2"))
					toReturn[2] += rTick;
			}
		}
		return toReturn;
	}
}
