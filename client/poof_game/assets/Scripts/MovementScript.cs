using UnityEngine;
using System.Collections;

/* MovementScript - handles movement of poofs across the screen. Apparently the way I did this 
 * was kind of a cop-out, so get rekt me. It maintains a queue of input positions
 * and pull those after completing a move. If a player gives an input, the queue is flushed and
 * the player's input is immediately run */

public class MovementScript : MonoBehaviour {
	// THIS NONSENSE NEEDS TO CHANGE ITS POOFSCRIPTS ONTILE AND STUFF
	
	// Vectors containing the starting and ending position for movement
	private Vector2 currentPos;
	private Vector2 targetPos;
	
	// Publicly defineable moveent speed; you can fiddle with it in the editor
	public float movementSpeed;
	
	// Progress accumulator used for lerping
	private float progressAccum;
	
	// Boolean flags that determine behavior
	private bool isMoving;
	private bool input;
	private bool priorityInput;
	private bool priorityComplete;
	
	// Queue that contains Vector2's of where the poof should be moving
	private Queue movementQueue;
	
	// Not utilized at all at the moment, because the passive mover script is incomplete
	private PassiveMover ps;
	
	void Start () {
	
		isMoving = false;
		input = false;
		priorityInput = false;
		priorityComplete = true;
		progressAccum = 0;
		
		movementQueue = new Queue();
		currentPos = new Vector2(transform.position.x, transform.position.y);
		targetPos = new Vector2();
		
		
		// In theory this block of code initializes the passivemover script simply as a script
		//		not as a monobehaviour component, and the loop is for passing in all poofs of the
		//		same tyoe as the current poof; which is for influencing its movement.
		GameObject[] poofs = GameObject.FindGameObjectsWithTag("Poof");
		ArrayList poofsOfSameType = new ArrayList();
		foreach (GameObject p in poofs) {
			if (p.GetComponent<PoofScript>().type == this.GetComponent<PoofScript>().type)
				poofsOfSameType.Add(p);
		}
		
		ps = new PassiveMover(poofsOfSameType);
	}
	
	void Update () {
	
		if (priorityInput) {
			getInputs();
		}
		
		if (isMoving)
			continueMoving();
		else {
			if (!input)
				getInputs();
			else
				startMoving();
		}
	}
	
	// Pops the first element out of the queue
	private void getInputs() {
		if (movementQueue.Count > 0) {
			targetPos = (Vector2)movementQueue.Dequeue();
			input = true;
		}
	}
	
	// Physically does the lerping of the poof to make the actual movement
	private void continueMoving() {
		
		float totalDistance = Vector2.Distance(currentPos, targetPos);
		float currentDistance = Vector2.Distance(currentPos, transform.position);
		
		progressAccum = (currentDistance + movementSpeed) / totalDistance;
		
		if (progressAccum <= 1)
			transform.position = Vector2.Lerp(currentPos, targetPos, progressAccum);
		else {
			stopMoving();
			if (priorityInput)
				priorityComplete = true;
		}
	}
	
	// Resets all variables and halts movement progress from passive inputs
	private void stopMoving() {
		if (isMoving && priorityComplete) {
			currentPos = targetPos;
			isMoving = false;
			priorityInput = false;
			input = false;
			progressAccum = 0;
			targetPos = new Vector2();
			movementQueue.Clear();
		}
		else if (priorityInput) {
			currentPos = transform.position;
			progressAccum = 0;
			movementQueue.Clear ();
		}
	}
	
	// Only sets the isMoving flag to true, and also determines if the movement is a priority input
	private void startMoving() {
		isMoving = true;
		if (priorityInput)
			priorityComplete = false;
	}
	
	// Method that Enqueues an input direction; should be utilized by the passive mover script
	public void receivePassiveInputs(Vector2 direction) {
		movementQueue.Enqueue(direction);
	}
	
	// Method that Enqueues player inputs and interrupts current passive inputs
	public void receivePlayerInputs(Vector2 direction) {
		priorityInput = true;
		stopMoving();
		movementQueue.Enqueue(direction);
	}
	
	// Getter method that returns the isMoving flag
	public bool getMoving() {
		return isMoving;
	}
	
	// method that sets the scripts current position field
	//		currently not in use by anything, and probably shouldn't be in use
	public void setCurrentPosition(Vector2 cp) {
		currentPos = cp;
	}
}
