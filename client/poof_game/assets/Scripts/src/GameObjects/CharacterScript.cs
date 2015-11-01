using UnityEngine;
using System.Collections;

/* CharacterScript - contains all the information pertaining to the character
 * such as where they are, their type, and information for how they
 * can move */

public class CharacterScript : MonoBehaviour {

    // Enum to identify type
	public enum Element {Fire, Water, Wind, Earth};
	
	// Enumeration defining the type of each character
	public Element type;
	
	// References to the movement based scripts for the character
	private MovementScript ms;
	private PassiveMoverPoofs ps;
	
	// Tile that the poof is currently standing on
	public Tile onTile { get; set;}
	
    /**
     * Initializes this object
     */
	void Start() {
		ms = this.GetComponent<MovementScript>();
		ps = this.GetComponent<PassiveMoverPoofs>();
        onTile = TileScript.grid.tiles[(int)Random.Range(0, TileScript.grid.tiles.Length - 1)];
	}
	
    /**
     * Changes the game state after every frame
     */
	void Update() {
		if (!ms.getMoving()) {
			if (!IsInvoking()) {
				Invoke("startMovement", 3f);
			}
		}
	}
	
	void startMovement() {
		ms.receivePassiveInputs(ps.getNewTile());
	}
}
