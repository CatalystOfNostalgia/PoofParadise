using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
/**
 * Let's add a settings menu. It will be used to control music mainly.
 * This menu should
 * 1) Allow the user to change the song
 * 2) Set a single/not play other songs
 * 3) Adjust the volume ratio between sound effects and music.
 */
public class SettingsMenu : MonoBehaviour {

	public Button playButtonAir;
	public Button playButtonEarth;
	public Button playButtonFire;
	public Button playButtonWater;
    public Button nextSong;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

	public Button exit;

    /**
     * Adds functionality to all of the buttons on the panel
     */
	public void generatePanel(){
		this.gameObject.SetActive (true);
		AudioSource[] music = SoundManager.soundManager.playlist;

		playButtonAir.onClick.RemoveAllListeners ();
		playButtonAir.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonAir.name));

		playButtonEarth.onClick.RemoveAllListeners ();
		playButtonEarth.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonEarth.name));

		playButtonFire.onClick.RemoveAllListeners ();
		playButtonFire.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonFire.name));

		playButtonWater.onClick.RemoveAllListeners ();
		playButtonWater.onClick.AddListener (() => SoundManager.soundManager.playSong(playButtonWater.name));

		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (ClosePanel);

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

    void ClosePanel(){
		this.gameObject.SetActive(false);
	}
}
