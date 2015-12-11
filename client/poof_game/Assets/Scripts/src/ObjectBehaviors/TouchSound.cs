using UnityEngine;
using System.Collections;

public class TouchSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    /**
 * A method that works with
 * the collider of this object
 */
    void OnMouseDown()
    {
        SoundManager.soundManager.playSoundEffect("poof1");
    }

    /**
     * A method that works with
     * the collider of this object
     */
    void OnMouseEnter()
    {
    }

    /**
     * A method that works with
     * the collider of this object
     */
    private void OnMouseExit()
    {

    }
}
