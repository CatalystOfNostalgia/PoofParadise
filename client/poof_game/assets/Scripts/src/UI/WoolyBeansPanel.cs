using UnityEngine.UI;
using UnityEngine;


public class WoolyBeansPanel : GamePanel {

	private Button b;
	
	override public void Start()
	{
		b = this.GetComponentInChildren<Button>();
		GeneratePanel();
	}
	
	override public void GeneratePanel(){
		
		b.onClick.RemoveAllListeners();
		b.onClick.AddListener(() => MicrotransactionPanel.mp.TogglePanel());
	}
	
	public void updateBeanCount() {
		this.GetComponentInChildren<Text>().text = "" + SaveState.state.woolyBeans;
	}
}
