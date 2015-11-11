using UnityEngine.UI;

/**
 * Let's add a settings menu. It will be used to control music mainly.
 * This menu should
 * 1) Allow the user to change the song
 * 2) Set a single/not play other songs
 * 3) Adjust the volume ratio between sound effects and music.
 */
public class SettingsMenu : GamePanel {

    // A static reference to this object
    public static SettingsMenu menu;

    private Button[] buttons;
    private Slider[] sliders;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;

	public Button exit;

    /**
     * Generates references based on children
     */
    override public void Start()
    {
        buttons = RetrieveButtonList("Dialogue Panel/Buttons");
        sliders = RetrieveSliderList("Dialogue Panel/Sliders");
        GeneratePanel();
    }

    /**
     * Adds functionality to all of the buttons on the panel
     */
	override public void GeneratePanel(){

        // Locates the button and gives it a function
        // TODO: Change this function such that a search is not needed but rather that songs can be call based on button names
        FindAndModifyButton("Air Theme", buttons, () => SoundManager.soundManager.playSong("Air Theme"));

        FindAndModifyButton("Earth Theme", buttons, () => SoundManager.soundManager.playSong("Earth Theme"));

        FindAndModifyButton("Fire Theme", buttons, () => SoundManager.soundManager.playSong("Fire Theme"));

        FindAndModifyButton("Water Theme", buttons, () => SoundManager.soundManager.playSong("Water Theme"));

        FindAndModifyButton("Exit Button", buttons, TogglePanel);

        FindAndModifyButton("Next Song", buttons, () => SoundManager.soundManager.nextSong());

        // It would probably be easier to just write a function for the slider, but...
        masterVolumeSlider.onValueChanged.RemoveAllListeners();
        masterVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.masterVolume = masterVolumeSlider.value; });

        musicVolumeSlider.onValueChanged.RemoveAllListeners();
        musicVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.musicVolume = musicVolumeSlider.value; });

        soundVolumeSlider.onValueChanged.RemoveAllListeners();
        soundVolumeSlider.onValueChanged.AddListener(delegate { SoundManager.soundManager.soundVolume = soundVolumeSlider.value; });
    }


}
