using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

/**
 * Let's add a settings menu. It will be used to control music mainly.
 * This menu should
 * 1) Allow the user to change the song
 * 2) Set a single/not play other songs
 * 3) Adjust the volume ratio between sound effects and music.
 */
public class SettingsMenu : MonoBehaviour {

	private Button playButtonAir;
	private Button playButtonEarth;
	private Button playButtonFire;
	private Button playButtonWater;
    private Button nextSong;

    private Button[] buttons;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

	public Button exit;

    /**
     * Generates references based on children
     */
    void Start()
    {
        List<Button> list = new List<Button>();
        // Generates a list of buttons from the children of this object
        foreach (Transform t in this.transform.GetChild(0))
        {
            list.Add(t.GetComponent<Button>());
        }
        buttons = list.ToArray();
        GeneratePanel();
    }

    /**
     * Adds functionality to all of the buttons on the panel
     */
	public void GeneratePanel(){
		this.gameObject.SetActive (true);
		AudioSource[] music = SoundManager.soundManager.playlist;

        // Locates the button and gives it a function
        // TODO: Change this function such that a search is not needed but rather that songs can be call based on button names
        FindAndModifyButton("Air Theme", buttons, ref playButtonAir, () => SoundManager.soundManager.playSong(playButtonAir.name));

        FindAndModifyButton("Earth Theme", buttons, ref playButtonEarth, () => SoundManager.soundManager.playSong(playButtonEarth.name));

        FindAndModifyButton("Fire Theme", buttons, ref playButtonFire, () => SoundManager.soundManager.playSong(playButtonFire.name));

        FindAndModifyButton("Water Theme", buttons, ref playButtonWater, () => SoundManager.soundManager.playSong(playButtonWater.name));

        exit = FindButton("Exit Button", buttons);
		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (ClosePanel);

        nextSong = FindButton("Next Song", buttons);
        nextSong.onClick.RemoveAllListeners();
        nextSong.onClick.AddListener(() => SoundManager.soundManager.nextSong());

        // It would probably be easier to just write a function for the slider, but...
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        masterVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.masterVolume = masterVolumeSlider.value; });

        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.musicVolume = musicVolumeSlider.value; });

        soundVolumeSlider.onValueChanged.RemoveAllListeners();
        soundVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.soundVolume = soundVolumeSlider.value; });
    }

    /**
     * Searches for a button in a list
     * Removes the listeners on that button
     * Adds a listener to that button
     */
    private void FindAndModifyButton(string name, Button[] list, ref Button local, UnityAction method)
    {
        // Runs a search for a button by name
        local = FindButton(name, list);

        // Lets the user know that their button doesn't exist
        if (local == null)
        {
            Debug.LogError("FindAndModifyButton failed to find " + name + " in button list");
        }

        // Removes all listeners and adds functionality
        local.onClick.RemoveAllListeners();
        local.onClick.AddListener(method);
    }

    /**
     * Returns a button by name
     */
    private Button FindButton(string button, Button[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].name == button)
            {
                return list[i];
            }
        }
        return null;
    }

    void ClosePanel(){
        this.gameObject.SetActive(false);
	}
}
