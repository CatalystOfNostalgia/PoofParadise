using UnityEngine;
using UnityEngine.UI;

public class LeaderPanel : GamePanel {
	
	// Stores a static reference to this object
	public static LeaderPanel leaderPanel;
	
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
		GeneratePanel();
	}
	
	/**
     * A function which gives all of the properties to the
     * buttons on this panel
     */
	override public void GeneratePanel()
	{
		//nothing happens since we haven't established database connection yet
	}
	
	/**
     * Apparently this links some text
     * TODO: Figure out its importance
     */
}


