using UnityEngine;
using UnityEngine.UI;

public class ModelPanel : GamePanel {

    // Stores a static reference to this object
    public static ModelPanel modelPanel;

    private Button[] buttons;

    public Text question;
	public Image iconImage;

    /**
     * Initializes panel
     */
    override public void Start()
    {
        Debug.Log("Model Panel is Active");
        buttons = RetrieveButtonList("Model Dialogue Panel/Buttons");
        GeneratePanel();
    }

    /**
     * A function which gives all of the properties to the
     * buttons on this panel
     */
    override public void GeneratePanel()
    {
        // Adds listener to all buttons
        foreach (Button b in buttons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(ClosePanel);
            b.gameObject.SetActive(true);
        }
    }

    public void Choice(string question){
		//model panel should be visible on screen
		this.gameObject.SetActive (true);



		this.question.text = question; //sets question in dialogue box
		//this.iconImage.gameObject.SetActive (false);
	}
	
	void ClosePanel(){
		modelPanel.gameObject.SetActive(false);
	}
}
