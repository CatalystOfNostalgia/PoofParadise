using UnityEngine;
using System.Collections;

/* PoofScript - contains all the information pertaining to the poofs
 * such as where they are, their type, and information for how they
 * can move */

public class PoofScript : MonoBehaviour {

	public enum Element {Fire, Water, Wind, Earth};
	
	// Enumeration defining the type of each poof
	public Element type;
	
	// References to the movement based scripts for the poof
	//		currently PassiveMover is inoperable and is not doing anything
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
	}
	
	void Update() {
		// Testing purposes only // This really isn't how this should work because the MovementScript
									// should be updating the onTile instead of this script
		// Z is bound to an arbitrary passive movement
		if (Input.GetKeyDown(KeyCode.Z) && !ms.getMoving()) {
			GameObject[] adjacents = grid.GetComponent<TileScript>().getAdjacentTiles(onTile);
			Vector2 input = adjacents[7].transform.position;
			ms.receivePassiveInputs(input);
			onTile = adjacents[7];
		}
		// X is bound to an arbitrary player movement => goToTile
		if (Input.GetKeyDown(KeyCode.X)) {
			Vector2 input = goToTile.transform.position;
			ms.receivePlayerInputs(input);
			onTile = goToTile;
		}
	}
}
