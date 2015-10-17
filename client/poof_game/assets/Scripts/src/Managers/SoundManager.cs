using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource[] playlist { get; set;}
	public AudioSource currentSong {get;}
	bool currentSongPlayed;
	public float musicVolume { get; set; }

	int index;
	private static SoundManager soundManager;

	public static SoundManager Instance(){
		if (!soundManager) {
			soundManager = FindObjectOfType(typeof (SoundManager)) as SoundManager;
			if(!soundManager)
				Debug.LogError ("There needs to be one active SoundManager script on a GameObject in your scene.");
		}
		return soundManager;
	}

	public AudioSource[] getAvailableMusic(){
		return this.GetComponentsInChildren<AudioSource> ();
	}

	public void stopSong(){
		currentSong.Stop ();
	}

	public void nextSong(){
		stopSong (); //might be redundant
		currentSongPlayed = false;
		index++;
		currentSong = playlist[index % playlist.Length];
	}

	public void previousSong(){
		stopSong (); //might be redundant
		currentSongPlayed = false;
		index--;
		currentSong = playlist[index % playlist.Length];
	}

	// Use this for initialization
	void Start () {
		index = 0;

		//in the future, load user's music preference
		playlist = getAvailableMusic();
		musicVolume = 100f;

		currentSong = playlist [index];
		currentSongPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!currentSong.isPlaying) {
			//didn't start the song yet
			if (!currentSongPlayed){
				currentSong.Play();
				currentSongPlayed = true;
			}
			else {//finished the song
				nextSong();
			}
		}
	}
}
