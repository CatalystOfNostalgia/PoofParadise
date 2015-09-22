using UnityEngine;
using System.Collections;
// This should be a passive class that calculates the next random movement for a poof based on
//		1. the poofs nearest likeable object
//		2. the most recent movement (to dissuade back and forth motions)
//		3. a boredom factor (incites the poof to move around the map and reset)
public class PassiveMover {

	public enum Direction {Down_Left, Down, Down_Right, Left, Right, Up_Left, Up, Up_Right};
	
	// Super duper work in progress.
	
	private Direction mostRecent;
	//private bool bored;
	
	private ArrayList likes;
	
	public PassiveMover (ICollection c) {
		likes = new ArrayList(c);
		//bored = false;
	}
	
	
	
}
