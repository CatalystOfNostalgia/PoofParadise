using UnityEngine.UI;
using UnityEngine;

/**
 * TODO: Add a description
 */
public class PoofCounterPanel : GamePanel {

    public static PoofCounterPanel poofCounterPanel;
    private Text poofCounterText;

    /**
     * Overrides the basic start functionality
     * for a game panel
     */
	override public void Start()
	{
        poofCounterText = GetComponentInChildren<Transform>().GetComponentInChildren<Image>().GetComponentInChildren<Text>();
        poofCounterText.fontSize = 40;
		GeneratePanel();
	}
	
    /**
     * Resets text on this panel
     */
	override public void GeneratePanel(){
        poofCounterText.text = string.Format("{0}/{1}", SaveState.state.poofCount, SaveState.state.poofLimit);
	}
}
