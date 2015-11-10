using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    }

    /**
     * Adds functionality to all of the buttons on the panel
     */
	public void generatePanel(){
		this.gameObject.SetActive (true);
		AudioSource[] music = SoundManager.soundManager.playlist;

        playButtonAir = FindButton("Air Theme", buttons);
		playButtonAir.onClick.RemoveAllListeners ();
		playButtonAir.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonAir.name));

        playButtonEarth = FindButton("Earth Theme", buttons);
		playButtonEarth.onClick.RemoveAllListeners ();
		playButtonEarth.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonEarth.name));

        playButtonFire = FindButton("Fire Theme", buttons);
		playButtonFire.onClick.RemoveAllListeners ();
		playButtonFire.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonFire.name));

        playButtonWater = FindButton("Water Theme", buttons);
		playButtonWater.onClick.RemoveAllListeners ();
		playButtonWater.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonWater.name));

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
     * Returns a button by name
     */
    private Button FindButton(string button, Button[] buttons)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == button)
            {
                return buttons[i];
            }
        }
        return null;
    }

    void ClosePanel(){
        this.gameObject.SetActive(false);
	}
}
