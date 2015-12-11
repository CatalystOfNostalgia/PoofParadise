using UnityEngine;
using System.Collections;

public class TouchSound : MonoBehaviour {

	// Use this for initialization
	void Start () {

        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = .25f;
    }
    
    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "Animated_Fire(Clone)":
                SoundManager.soundManager.playFireSound();
                break;
            case "Animated_Water(Clone)":
                SoundManager.soundManager.playWaterSound();
                break;
            case "Animated_Earth(Clone)":
                SoundManager.soundManager.playEarthSound();
                break;
            case "Animated_Air(Clone)":
                SoundManager.soundManager.playAirSound();
                break;
            case "Animated_Poof(Clone)":
                SoundManager.soundManager.playPoofSound();
                break;
            default:
                Debug.Log("[TouchSound] Illegal Character in the Character Nest");
                break;
        }
    }
}
