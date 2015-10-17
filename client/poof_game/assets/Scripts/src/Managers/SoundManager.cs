using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public Dictionary<string, AudioSource> playDict{ get; set;}
	public AudioSource[] playlist { get; set;}
	public AudioSource currentSong { get; set;}
	bool currentSongPlayed;
	bool isPlayingSpecialRequestSong;
	public float musicVolume { get; set; }

	int index;

	public AudioSource[] getAvailableMusic(){
		return this.GetComponentsInChildren<AudioSource> ();
	}

	public void playSong(string songName){
		isPlayingSpecialRequestSong = true;
		AudioSource song;
		if (playDict.TryGetValue (songName, out song)) {
			Debug.Log("SoundManager: now playing " + songName);
			stopSong ();
			currentSong = song;
			currentSong.Play ();
			currentSongPlayed = true;
		} else {
			Debug.Log ("SoundManager: The key " + songName + " was not found in the dictionary");
		}
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
		playDict = new Dictionary<string, AudioSource> ();
		foreach (AudioSource music in playlist) {
			playDict.Add(music.name, music);
			Debug.Log("SoundManager: Added " + music.name + " to the dictionary");
		}
		musicVolume = 100f;

		currentSong = playlist [index];
		currentSongPlayed = false;
		isPlayingSpecialRequestSong = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!currentSong.isPlaying && isPlayingSpecialRequestSong) {
			if (!currentSongPlayed){
				return;
			}
		}
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
