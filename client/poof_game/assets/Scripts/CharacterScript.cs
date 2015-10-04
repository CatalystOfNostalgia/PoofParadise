using UnityEngine;
using System.Collections;

/* CharacterScript - contains all the information pertaining to the character
 * such as where they are, their type, and information for how they
 * can move */

public class CharacterScript : MonoBehaviour {

	public enum Element {Fire, Water, Wind, Earth};
	
	// Enumeration defining the type of each character
	public Element type;
	
	// References to the movement based scripts for the character
	private MovementScript ms;
	private PassiveMover ps;
	
	// The grid object that contains all the tiles
	public GameObject grid;
	// Tile that the poof is currently standing on
	public GameObject onTile;
	// Debugging tile 
	public GameObject goToTile;
	
	void Start() {
		ms = this.GetComponent<MovementScript>();
		ps = this.GetComponent<PassiveMover>();
	}
	
	void Update() {
		// Testing purposes only //
		// Z is bound to an arbitrary passive movement

		//if (Input.GetKeyDown(KeyCode.Z) && !ms.getMoving()) {
		if (!ms.getMoving()) {
			ms.receivePassiveInputs(ps.getNewTile());
		}
		// X is bound to an arbitrary player movement => goToTile
		if (Input.GetKeyDown(KeyCode.X)) {
			ms.receivePlayerInputs(goToTile);
		}
	}
	
	public void setOnTile (GameObject tile) {
		onTile = tile;
	}
	
	public GameObject getOnTile() {
		return onTile;
	}
	
	public GameObject getGrid() {
		return grid;
	}
}
