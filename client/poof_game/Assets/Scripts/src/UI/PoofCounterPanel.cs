using UnityEngine.UI;
using UnityEngine;


public class PoofCounterPanel : GamePanel {
    public static PoofCounterPanel poofCounterPanel;
    private Text poofCounterText;

	override public void Start()
	{
        poofCounterText = RetrieveTextList("Buttons/PoofCounter")[0];
		GeneratePanel();
	}
	
	override public void GeneratePanel(){
        poofCounterText.text = string.Format("{0}/{1}", SaveState.state.poofCount, SaveState.state.poofLimit);
	}

    void Update()
    {
        GeneratePanel();
    }
}
