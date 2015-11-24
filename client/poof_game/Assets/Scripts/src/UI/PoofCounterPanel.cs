using UnityEngine.UI;
using UnityEngine;


public class PoofCounterPanel : GamePanel {
	public int poofCount;
	
	override public void Start()
	{
		poofCount = 0;
		GeneratePanel();
	}
	
	override public void GeneratePanel(){

		updateBeanCount ();
	}
	
	public void updateBeanCount() {
		if (poofCount != SaveState.state.poofCount) {
			this.GetComponentInChildren<Text> ().text = "" + SaveState.state.poofCount;
		}
		poofCount = SaveState.state.poofCount;
	}
}
