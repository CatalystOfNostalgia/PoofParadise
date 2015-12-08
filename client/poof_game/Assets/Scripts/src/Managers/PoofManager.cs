using UnityEngine;
using System.Collections;

public class PoofManager : MonoBehaviour {

    public int poofGenerationRate { get; set; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //periodically roll a dice based on poof generation rate to spawn a poof
        // could base it off of how many poofs exist vs. how many poofs we alreayd have
        // like Delta poof * poofRate?
        
	}
}
