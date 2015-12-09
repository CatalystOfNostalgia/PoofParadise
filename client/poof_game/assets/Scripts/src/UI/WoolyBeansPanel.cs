using UnityEngine.UI;
using UnityEngine;

/**
 * The wooly bean panel which displays the total number of 
 * wooly beans the user has
 */
public class WoolyBeansPanel : GamePanel {

	private Button b;
	
    /**
     * Overrides the start functionality 
     * of GamePanel
     */
	override public void Start()
	{
		b = this.GetComponentInChildren<Button>();
		GeneratePanel();
	}
	
    /**
     * Overrides the generate panel functionality
     * of GamePanel
     */
	override public void GeneratePanel(){
		
		b.onClick.RemoveAllListeners();
		b.onClick.AddListener(() => MicrotransactionPanel.mp.TogglePanel());
	}
	
    /**
     * Updates the bean count
     */
	public void updateBeanCount() {
		this.GetComponentInChildren<Text>().text = "" + SaveState.state.woolyBeans;
	}
}
