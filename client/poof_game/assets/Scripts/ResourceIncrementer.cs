using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//currently no player game object, could be slider...
public class ResourceIncrementer : MonoBehaviour
{
	public int startingNumRes;                            // The amount of resources the player starts the game with.
	public int currentNumRes;                                   // The current number of resources the player has.
	public Slider resSlider;                                    // Reference to the UI's resource bar.                                
	public bool gain;   // True when the gains resources
	int amount;
	
	void Awake ()
	{
		// Setting up the references.
		//resCount = GetComponent <ResCount> ();
		// Set the initial health of the player.
		currentNumRes = startingNumRes;
		resSlider.value = currentNumRes;
		gain = true;
	}
	
	
	public void ResourceGain ()
	{
		if (currentNumRes <= 100) {
			// Set the damaged flag so the screen will flash.
			
			// Reduce the current health by the damage amount.
			int amount = 1; 
			currentNumRes = currentNumRes + amount;
			// Set the health bar's value to the current health.
			resSlider.value = currentNumRes;
			
			// Play the hurt sound effect.
			//playerAudio.Play ();
		} 
		else
			currentNumRes = 100;
	} 
}
