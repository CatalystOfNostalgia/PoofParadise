using UnityEngine;
using System.Collections;

/* PoofScript - contains all the information pertaining to the poof gameobjects
 * such as where they are, their type, and information for how they
 * can move */

public class PoofScript : NPC {
	
	// Enum to identify type
	public enum Element {Fire, Water, Wind, Earth};
	
	// Enumeration defining the type of each character
	public Element type;
	
	// References to the movement based scripts for the character
	private MovementScript ms;
	private PassiveMoverPoofs pp;
	
	// Tile that the poof is currently standing on
	public Tile onTile { get; set;}
	
	/**
     * Initializes this object
     */
	void Start() {
		ms = this.GetComponent<MovementScript>();
		pp = this.GetComponent<PassiveMoverPoofs>();
	}
	
	/**
     * Changes the game state after every frame
     */
	void Update() {
		if (!ms.getMoving() && ms.isQueueEmpty()) {
			if (!IsInvoking()) {
				Invoke("startMovement", 3f);
			}
		}
	}
	
	void startMovement() {
		ms.receivePassiveInputs(pp.getNewTile());
	}
}
