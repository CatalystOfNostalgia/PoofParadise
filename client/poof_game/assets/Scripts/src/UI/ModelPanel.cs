using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ModelPanel : GamePanel {

	public Text question;
	public Image iconImage;
	public static ModelPanel modelPanel;

    /**
     * Initializes panel
     */
    override public void Start()
    {
        Debug.Log("Model Panel is Active");
    }

    public void Choice(string question){
		//model panel should be visible on screen
		this.gameObject.SetActive (true);

        // Adds listener to all buttons
        foreach (Button b in PrefabManager.prefabManager.buttons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(ClosePanel);
            b.gameObject.SetActive(true);
        }

		this.question.text = question; //sets question in dialogue box
		//this.iconImage.gameObject.SetActive (false);
	}
	
	void ClosePanel(){
		modelPanel.gameObject.SetActive(false);
	}
}
