﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

/**
 * A manager that deals with music
 */
public class SoundManager : Manager {

    // Singleton field
    public static SoundManager soundManager;

    // Fields
	public Dictionary<string, AudioSource> playDict{ get; set;}
	public AudioSource[] playlist { get; set;}
	public AudioClip[] soundEffects {get; set;}
	public Dictionary<string, AudioClip> effectDict {get;set;}
	private AudioSource soundEffectSource;

	public bool[] preferredPlaylist { get; set; }
	public AudioSource currentSong { get; set;}
	private bool currentSongPlayed;
	private bool allSongsDisabled;

	// boolean to block new songs while song is changing
	private bool changingSong; 

	// this volume field gets changed by a slider in the scene
	// should we create an explicit script to do so or is it fine to let generic unity slider to change it?
	public float masterVolume {get; set; }
	public float musicVolume { get; set; } 
	public float soundVolume { get; set; }

	int index;

    /**
     * Initializes SoundManager as a singleton
     *
     * Initializes song dictionary
     */
    override public void Start()
    {
        // Converts SoundManager into a singleton
        if (soundManager == null)
        {
            DontDestroyOnLoad(gameObject);
            soundManager = this;
        }
        else if (soundManager != this)
        {
            Destroy(gameObject);
        }

        index = 0;

        //in the future, load user's music preference
        playlist = getAvailableMusic();
		soundEffects = getAvailableEffects ();
		soundEffectSource = gameObject.AddComponent (typeof(AudioSource)) as AudioSource;


        preferredPlaylist = new bool[playlist.Length];
        for (int i = 0; i < preferredPlaylist.Length; i++)
        {
            preferredPlaylist[i] = true;
        }
        playDict = new Dictionary<string, AudioSource>();
        foreach (AudioSource music in playlist)
        {
            playDict.Add(music.name, music);
        }
		effectDict = new Dictionary<string, AudioClip> ();
		foreach (AudioClip clip in soundEffects) 
		{
			effectDict.Add(clip.name, clip);
		}
        masterVolume = 1f;
        musicVolume = 1f;
        soundVolume = 1f;

        currentSong = playlist[index];
        currentSongPlayed = false;
        allSongsDisabled = false;
    }

    /**
     * Generates the song list from the songs in children
     *
     * TODO: Pull songs from directory and generate tracklist
     */
    public AudioSource[] getAvailableMusic(){
		return this.GetComponentsInChildren<AudioSource> ();
	}

	public AudioClip[] getAvailableEffects(){
		return Resources.LoadAll ("Sound/Effects", typeof(AudioClip)).Cast<AudioClip> ().ToArray ();
	}

    /**
     * A function which takes a song name as 
     * input and plays that song
     */
	public void playSong(string songName){
		AudioSource song;
        // Play the song if it exists
        if (playDict.TryGetValue(songName, out song) && !changingSong)
        {
            changingSong = true;
            stopSong();
            currentSong = song;
            currentSong.Play();
            currentSongPlayed = true;
            changingSong = false;
        }

        // Otherwise, report the error
        else
        {
            Debug.LogError("[SoundManager] The key " + songName + " was not found in the dictionary");
        }
	}

    /**
     * A function which stops that song that
     * is currently playing
     */
	public void stopSong(){
		if (currentSong == null) {
			return;
		}
		currentSong.Stop ();
	}

    /**
     * A function which plays the next song
     * in the playlist
     */
	public void nextSong(){
		stopSong (); //might be redundant
		for (int i = 0; i<playlist.Length; i++) {
			currentSongPlayed = false;
			index++;
			if (preferredPlaylist[index % playlist.Length]){
				currentSong = playlist[index % playlist.Length];
				return;
			}
		}
		// This means all of the songs are disabled
		allSongsDisabled = true;
		Debug.Log ("[SoundManager] All music is disabled");
	}

    /**
     * A function which plays the previous song
     * in the playlist
     */
	public void previousSong(){
		stopSong (); //might be redundant
		for (int i = 0; i<playlist.Length; i++) {
			currentSongPlayed = false;
			index--;
			if (preferredPlaylist[index % playlist.Length]){
				currentSong = playlist[index % playlist.Length];
				return;
			}
		}
		// This means all of the songs are disabled
		allSongsDisabled = true;
		Debug.Log ("[SoundManager] All music are disabled");
	}

    /**
     * Not sure what this does
     * TODO: Write comment
     */
	public void setPreferredPlaylist (string song){
		//maybe I could have referenced the toggle to here and avoid the search
		int i = findIndex (song);
		if (i == -1) {
			return;
		}
		if (allSongsDisabled && !preferredPlaylist [i]) {
			allSongsDisabled = false;
		}
		preferredPlaylist [i] = !preferredPlaylist [i];
	}

    /**
     * Generates the index for a song name
     */
	private int findIndex (string songName){
		for (int i = 0; i<playlist.Length; i++){
			if (playlist[i].name.Equals(songName)){
				return i;
			}
		}
		return -1;
	}

	public void playSoundEffect (string name){
		AudioClip clip;
		if (effectDict.TryGetValue (name, out clip)) {
			soundEffectSource.PlayOneShot(clip, soundVolume * masterVolume);
		}
	}

	public void playConstruction (){
		playSoundEffect("EarthBuildingTruncated");
	}

	public void playButtonHigh (){
		playSoundEffect ("button_high");
	}

    public void playFireSound()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        Debug.Log("[SoundManager] RNG generated: " + rand);
        switch (rand)
        {
            case 0:
                playSoundEffect("fire1");
                break;
            case 1:
                playSoundEffect("fire2");
                break;
            case 2:
                playSoundEffect("fire3");
                break;
            default:
                Debug.Log("[SoundManager] RNG generated: " + rand);
                break;
        }
    }

    public void playWaterSound()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        Debug.Log("[SoundManager] RNG generated: " + rand);
        switch (rand)
        {
            case 0:
                playSoundEffect("water1");
                break;
            case 1:
                playSoundEffect("water2");
                break;
            case 2:
                playSoundEffect("water3");
                break;
            default:
                Debug.Log("[SoundManager] RNG generated: " + rand);
                break;
        }
    }

    public void playAirSound()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        Debug.Log("[SoundManager] RNG generated: " + rand);
        switch (rand)
        {
            case 0:
                playSoundEffect("air1");
                break;
            case 1:
                playSoundEffect("air2");
                break;
            default:
                Debug.Log("[SoundManager] RNG generated: " + rand);
                break;
        }
    }

    public void playEarthSound()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        Debug.Log("[SoundManager] RNG generated: " + rand);
        switch (rand)
        {
            case 0:
                playSoundEffect("earth1");
                break;
            case 1:
                playSoundEffect("earth2");
                break;
            case 2:
                playSoundEffect("earth3");
                break;
            default:
                Debug.Log("[SoundManager] RNG generated: " + rand);
                break;
        }
    }

    public void playPoofSound()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        Debug.Log("[SoundManager] RNG generated: " + rand);
        switch (rand)
        {
            case 0:
                playSoundEffect("poof1");
                break;
            case 1:
                playSoundEffect("poof2");
                break;
            default:
                Debug.Log("[SoundManager] RNG generated: " + rand);
                break;
        }
    }

    /**
     * Update is called once per frame
     */
    void Update () {
		if (allSongsDisabled) {
			currentSong = null;
			return;
		}
		if (currentSong == null) {
			return;
		}
		currentSong.volume = masterVolume * musicVolume;

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
