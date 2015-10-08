using UnityEngine;
using System.Collections;

public class ResourceBuilding : Building {

	// the rate at which gold is collected per second
	public int goldCollectionRate { get; set;}
	System.Timers.Timer resourceClock;

	// place the building
	public ResourceBuilding(int xCoord, int yCoord, int size) : base (xCoord, yCoord, size){
	}

	// Use this for initialization
	void Start () {

		// set the timer to increment the gold every second
		resourceClock = new System.Timers.Timer (1000);
		resourceClock.Elapsed += 
			(object sender, System.Timers.ElapsedEventArgs e) => {
				ResourceIncrementer.incrementer.ResourceGain (5);
			};
			

		resourceClock.Enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
