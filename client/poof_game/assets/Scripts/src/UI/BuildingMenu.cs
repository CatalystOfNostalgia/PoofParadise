using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingMenu : MonoBehaviour {
	
	public Text question;
	public Image iconImage;
	public GameObject modalPanelObject;
	
	private static BuildingMenu modalPanel;
	
	public static BuildingMenu Instance(){
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof (BuildingMenu)) as BuildingMenu;
			if(!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return modalPanel;
	}

	public void Choice(){
		//modal panel should be visible on screen
		modalPanelObject.SetActive (true);

        // Adds listener to all buttons
        foreach (Button b in PrefabManager.prefabManager.buttons)
        {
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(ClosePanel);
            b.gameObject.SetActive(true);
        }

		string buildquestion = "Select a Building";
		this.question.text = buildquestion; //sets question in dialogue box
		//this.iconImage.gameObject.SetActive (false);	
	}
	
	void ClosePanel(){
		modalPanelObject.SetActive(false);
	}
}
