using UnityEngine;
using UnityEngine.UI;

public class LeaderPanel : GamePanel {
	
	// Stores a static reference to this object
	public static LeaderPanel leaderPanel;

	private Button[] buttons;
	/**
     * Initializes panel
     */
	override public void Start()
	{
		if (leaderPanel == null) {
			DontDestroyOnLoad(gameObject);
			leaderPanel = this;
		} else if (leaderPanel != this) {
			Destroy(gameObject);
		}
		buttons = RetrieveButtonList ("Buttons");
		GeneratePanel ();
	}
	
	/**
     * A function which gives all of the properties to the
     * buttons on this panel
     */
	override public void GeneratePanel()
	{
		FindAndModifyUIElement ("Exit", buttons, TogglePanel);
	}

}


