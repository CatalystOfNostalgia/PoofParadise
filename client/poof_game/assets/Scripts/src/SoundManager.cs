using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	AudioSource[] child;
	int index;
	
	// Use this for initialization
	void Start () {
		index = 0;
		child = this.GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!child[index % child.Length].isPlaying && !child[(index + 1) % child.Length].isPlaying) {
			child[(index + 1) % child.Length].Play();
			index++;
		}
	}
}
