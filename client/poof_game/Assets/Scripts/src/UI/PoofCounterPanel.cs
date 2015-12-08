using UnityEngine.UI;
using UnityEngine;


public class PoofCounterPanel : GamePanel {
    public static PoofCounterPanel poofCounterPanel;
    private Text poofCounterText;

	override public void Start()
	{
        poofCounterText = GetComponentInChildren<Transform>().GetComponentInChildren<Image>().GetComponentInChildren<Text>();
		GeneratePanel();
	}
	
	override public void GeneratePanel(){
        poofCounterText.text = string.Format("{0}/{1}", SaveState.state.poofCount, SaveState.state.poofLimit);
	}
    
    void Update()
    {
        // DO NOT DO THIS -> Basically freezes the game
        // Instead: Find out who updates poofcount anc call Generate Panel from there
        GeneratePanel();
    }
}
