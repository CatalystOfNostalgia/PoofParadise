using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This will be a passive component that calculates the next random movement for a character based on
//		1. the character's nearest likeable object
//		2. the most recent movement (to dissuade back and forth motions)
//		3. a boredom factor (incites the poof to move around the map and reset)
public class PassiveMoverPoofs : MonoBehaviour {
	
	// Enumeration that makes debugging more interesting to look at
	public enum Direction {Down_Left, Down, Down_Right, Left, Stay, Right, Up_Left, Up, Up_Right};
	
	// References to the attached character's scripts
	// currently ms is not used, but might be in the future
	private CharacterScript cs;
	private MovementScript ms;

	// Field containing the most recently calculated tile for the character to move to
	private Tile nextTile { get; set; }
	
	// Public field used for debugging purposes in the editor, and also to mess with random movements
	//public Direction mostRecent;
	
	// bored facet hasn't been implemented yet; TO DO
	private bool bored;
	
	// List containing GameObjects that should affect the character's wandering patterns
	//		currently not implemented; TO DO
	private ArrayList likes;
	
	void Start() {
		cs = this.GetComponent<CharacterScript>();
		ms = this.GetComponent<MovementScript>();
		
		//mostRecent = Direction.Stay;
		likes = new ArrayList();

	}
	
	private void calculateNextTile() {
		
		if (likes.Count == 0) {
			if (Random.Range (0,2) > 0) {
				List<Tuple> tuples = TileScript.grid.GetPossiblePaths(cs.onTile.index); 
           		Tuple[] arr = tuples.ToArray();
           		Tuple next = arr[(int)Random.Range(0, arr.Length)];
            	Tile test = TileScript.grid.GetTile(next);
            	nextTile = test;
            }
            else {
            	nextTile = cs.onTile;
            }
		}
		else {
			int domain = likes.Count;
			// this is where the guided movement will come in
		}
	}
	
	public Tile getNewTile() {
		calculateNextTile();
		return nextTile;
	}
}
