using UnityEngine;
using System.Collections;

/* MovementScript - handles movement of poofs across the screen. Apparently the way I did this 
 * was kind of a cop-out, so get rekt me. It maintains a queue of input positions
 * and pull those after completing a move. If a player gives an input, the queue is flushed and
 * the player's input is immediately run */

public class MovementScript : MonoBehaviour {
	
	// Vectors containing the starting and ending position for movement
	private Vector2 currentPos;
	private Vector2 targetPos;
	private Tile targetObj;
	private Animator animator;
	
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
	
	private PassiveMoverCharacters pcp;
	private PassiveMoverPoofs pmp;
	private CharacterScript cs;
	private PoofScript ps;

	private float transitionTiming = 0f;
	
	void Start () {
	
		isMoving = false;
		input = false;
		priorityInput = false;
		priorityComplete = true;
		progressAccum = 0;
		
		movementQueue = new Queue();
		currentPos = new Vector2(transform.position.x, transform.position.y);
		targetPos = new Vector2();

		Debug.Log(currentPos);
		Debug.Log(targetPos);
		
		animator = this.GetComponent<Animator>();
	}
	
	public void initializePoof() {
		ps = this.GetComponent<PoofScript>();
		pmp = this.GetComponent<PassiveMoverPoofs>();
	}
	
	public void initializeCharacter() {
		cs = this.GetComponent<CharacterScript>();
		pcp = this.GetComponent<PassiveMoverCharacters>();

		if(cs.type == CharacterScript.Element.Wind){
			transitionTiming = 1.917f;
		}
		if(cs.type == CharacterScript.Element.Water){
			transitionTiming = 2.167f;
		}
		if(cs.type == CharacterScript.Element.Earth){
			transitionTiming = 2.0f;
		}
		if(cs.type == CharacterScript.Element.Fire){
			transitionTiming = 0.917f;
		}
	}
	
	void Update () {
		// Check to determine if animation changes are happening
		if (!IsInvoking()) {
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
	}
	
	// Pops the first element out of the queue
	private bool getInputs() {
		if (movementQueue.Count > 0) {
			targetObj = movementQueue.Dequeue() as Tile;
			if (targetObj != null) {
				targetPos = parseObjToLoc(targetObj);
				input = true;
				return true;
			}
			else
				return false;
		}
		else
			return false;
	}
	
	private void continueMoving() {
		if (animator != null)
		{
			if (currentPos.x - targetPos.x > 0 && currentPos.y - targetPos.y > 0)
			{
				animator.SetInteger("Direction", 0);
			}
			if (currentPos.x - targetPos.x < 0 && currentPos.y - targetPos.y > 0)
			{
				animator.SetInteger("Direction", 1);
			}
			if (currentPos.x - targetPos.x < 0 && currentPos.y - targetPos.y < 0)
			{
				animator.SetInteger("Direction", 2);
			}
			if (currentPos.x - targetPos.x > 0 && currentPos.y - targetPos.y < 0)
			{
				animator.SetInteger("Direction", 3);
			}
		}
		lerperoni();
	}
	
	// Physically does the lerping of the poof to make the actual movement
	private void lerperoni() {
		
		float totalDistance = Vector2.Distance(currentPos, targetPos);
		float currentDistance = Vector2.Distance(currentPos, transform.position);
		
		progressAccum = (currentDistance + movementSpeed) / totalDistance;
		
		if (progressAccum <= 1)
			transform.position = Vector3.Lerp(new Vector3(currentPos.x, currentPos.y, currentPos.y - .3f), new Vector3(targetPos.x, targetPos.y, targetPos.y), progressAccum);
		else {
			if (!isQueueEmpty()) {
				currentPos = targetPos;
				targetPos = new Vector2();
				targetPos = new Vector2();
				if (cs != null)
					cs.onTile = targetObj;
				else
					ps.onTile = targetObj;
				getInputs();
				continueMoving();
			}
			else
				stopMoving();
		}
	}
	
	// Resets all variables and halts movement progress from passive inputs
	private void stopMoving() {

		if (animator != null) {
			//Debug.Log("stopping to move");
			animator.SetInteger("Direction", 4);
			Invoke("animatorChange", transitionTiming);
		}
		if (isMoving) {
			currentPos = targetPos;
			isMoving = false;
			priorityInput = false;
			input = false;
			progressAccum = 0;
			targetPos = new Vector2();
			if (cs != null)
				cs.onTile = targetObj;
			else
				ps.onTile = targetObj;
			targetObj = null;
			movementQueue.Clear();
		}
		// special case for player input's that interrupt a passive movement
		else if (priorityInput) {
			currentPos = transform.position;
			progressAccum = 0;
			movementQueue.Clear ();
		}
	}
	
	// Only sets the isMoving flag to true, and also determines if the movement is a priority input
	private void startMoving() {
		Invoke("animatorChange", 1.917f);
		isMoving = true;
		if (priorityInput)
			priorityComplete = false;
		
        if (animator != null)
        {
			Invoke("animatorChange", transitionTiming);
            if (currentPos.x - targetPos.x > 0 && currentPos.y - targetPos.y > 0)
            {
                animator.SetInteger("Direction", 0);
            }
            if (currentPos.x - targetPos.x < 0 && currentPos.y - targetPos.y > 0)
            {
                animator.SetInteger("Direction", 1);
            }
            if (currentPos.x - targetPos.x < 0 && currentPos.y - targetPos.y < 0)
            {
                animator.SetInteger("Direction", 2);
            }
            if (currentPos.x - targetPos.x > 0 && currentPos.y - targetPos.y < 0)
            {
                animator.SetInteger("Direction", 3);
            }
        }
        else
        {
			//Debug.Log("Congratulations, this character doesn't have a animation");
        }

		isMoving = true;
		if (priorityInput)
			priorityComplete = false;
	}
	
	public void animatorChange() {
		//Debug.Log("Transition");
		animator.SetInteger("Direction", 4);
	}
	
	// Method that Enqueues an input direction; should be utilized by the passive mover script
	public void receivePassiveInputs(Tile toTile) {
		// Needs to parse the toTile into a set of adjacent tiles that can be travelled consecutively
		object[] toAdd = parseTileInput(toTile);
		
		for (int i = 0; i < toAdd.Length; i++) {
			movementQueue.Enqueue(toAdd[i]);
		}
	}
	
	// Method that Enqueues player inputs and interrupts current passive inputs
	//		errata'd out at the moment
	public void receivePlayerInputs(Tile toTile) {
		/*priorityInput = true;
		stopMoving();
		movementQueue.Enqueue(toTile);*/
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
	
	// Method that parses a GameObject into a Vector2 which is the input GameObject's position
	private Vector2 parseObjToLoc(Tile toTile) {
		return new Vector2(toTile.transform.position.x, toTile.transform.position.y);
	}
	
	public bool isQueueEmpty() {
		return movementQueue.Count == 0;
	}
	
	private object[] parseTileInput(Tile toTile) {
		ArrayList toReturn = new ArrayList();
		
		if (cs != null) {	
			int h = getHorizontalDistance(cs.onTile, toTile);
			int v = getVerticalDistance(cs.onTile, toTile);
			Tile nextTile = cs.onTile;
			
			if (h > 0) {
				for (int i = h; i >= 0; i--) {
					if (nextTile != null) {
						nextTile = nextTile.rightTile;
						toReturn.Add(nextTile);
					}
				}
			}
			else {
				for (int i = h; i <= 0; i++) {
					if (nextTile != null) {
						nextTile = nextTile.leftTile;
						toReturn.Add(nextTile);
					}
				}
			}
			
			if (v > 0) {
				for (int i = v; i >= 0; i--) {
					if (nextTile != null) {
						nextTile = nextTile.upTile;
						toReturn.Add(nextTile);
					}
				}
			}
			else {
				for (int i = v; i <= 0; i++) {
					if (nextTile != null) {
						nextTile = nextTile.downTile;
						toReturn.Add(nextTile);
					}
				}
			}
			
			return toReturn.ToArray();
		}
		else {
			int h = getHorizontalDistance(ps.onTile, toTile);
			int v = getVerticalDistance(ps.onTile, toTile);
			Tile nextTile = ps.onTile;
			
			if (h > 0) {
				for (int i = h; i >= 0; i--) {
					if (nextTile != null) {
						nextTile = nextTile.rightTile;
						toReturn.Add(nextTile);
					}
				}
			}
			else {
				for (int i = h; i <= 0; i++) {
					if (nextTile != null) {
						nextTile = nextTile.leftTile;
						toReturn.Add(nextTile);
					}
				}
			}
			
			if (v > 0) {
				for (int i = v; i >= 0; i--) {
					if (nextTile != null) {
						nextTile = nextTile.upTile;
						toReturn.Add(nextTile);
					}
				}
			}
			else {
				for (int i = v; i <= 0; i++) {
					if (nextTile != null) {
						nextTile = nextTile.downTile;
						toReturn.Add(nextTile);
					}
				}
			}
			
			return toReturn.ToArray();
		}
	}
	
	private int getHorizontalDistance(Tile t1, Tile t2) {
		int x1 = t1.index.x;
		int x2 = t2.index.x;
		return x2 - x1;
	}
	
	private int getVerticalDistance(Tile t1, Tile t2) {
		int y1 = t1.index.y;
		int y2 = t2.index.y;
		return y2 - y1;
	}
}
