using UnityEngine;
using System.Collections;

public class ResourceBuilding : Building {

	public enum ResourceType { fire, water, air, earth };

	// the resource collection rate and the type of resource
	public int collectionRate { get; set;}
	private ResourceType type;
	System.Timers.Timer resourceClock;

	// place the building
	public ResourceBuilding(int xCoord, int yCoord, int size, int collectionRate, ResourceType type) : base (xCoord, yCoord, size){
		this.collectionRate = collectionRate;
		this.type = type;
	}

	// Use this for initialization
	void Start () {

		collectionRate = 5;
		// set the timer to increment the gold every second
		resourceClock = new System.Timers.Timer (1000);
		resourceClock.Elapsed += 
			(object sender, System.Timers.ElapsedEventArgs e) => {
				ResourceIncrementer.incrementer.ResourceGain (collectionRate);
			};
			

		resourceClock.Enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
