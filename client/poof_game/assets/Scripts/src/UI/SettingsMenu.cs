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
        FindAndModifyUIElement("Air Theme", buttons, () => SoundManager.soundManager.playSong("Air Theme"));

        FindAndModifyUIElement("Earth Theme", buttons, () => SoundManager.soundManager.playSong("Earth Theme"));

        FindAndModifyUIElement("Fire Theme", buttons, () => SoundManager.soundManager.playSong("Fire Theme"));

        FindAndModifyUIElement("Water Theme", buttons, () => SoundManager.soundManager.playSong("Water Theme"));

        FindAndModifyUIElement("Exit Button", buttons, TogglePanel);

        FindAndModifyUIElement("Next Song", buttons, () => SoundManager.soundManager.nextSong());

        FindAndModifyUIElement("Master Volume Slider", sliders, delegate { SoundManager.soundManager.masterVolume = sliders[FindUIElement("Master Volume Slider", sliders)].value; });

        FindAndModifyUIElement("Music Volume Slider", sliders, delegate { SoundManager.soundManager.musicVolume = sliders[FindUIElement("Music Volume Slider", sliders)].value; });

        FindAndModifyUIElement("Sound Volume Slider", sliders, delegate { SoundManager.soundManager.soundVolume = sliders[FindUIElement("Sound Volume Slider", sliders)].value; });
    }


}
