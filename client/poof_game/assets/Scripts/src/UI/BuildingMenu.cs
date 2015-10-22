using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingMenu : MonoBehaviour {
	
	public Text question;
	public Image iconImage;
	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button exit;
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
		
		button1.onClick.RemoveAllListeners ();
		button1.onClick.AddListener (ClosePanel);
		//button1.onClick.AddListener (() => BuildingManager.Instance().makeNewBuilding(1));
		
		
		button2.onClick.RemoveAllListeners ();
		button2.onClick.AddListener (ClosePanel);
		//button2.onClick.AddListener (() => BuildingManager.Instance().makeNewBuilding(2));
		
		
		button3.onClick.RemoveAllListeners ();
		button3.onClick.AddListener (ClosePanel);
		//button3.onClick.AddListener (() => BuildingManager.Instance().makeNewBuilding(3));
		
		button4.onClick.RemoveAllListeners ();
		button4.onClick.AddListener (ClosePanel);
		//button4.onClick.AddListener (() => BuildingManager.Instance().makeNewBuilding(4));
		
		exit.onClick.RemoveAllListeners ();
		exit.onClick.AddListener (ClosePanel);

		string buildquestion = "Select a Building";
		this.question.text = buildquestion; //sets question in dialogue box
		//this.iconImage.gameObject.SetActive (false);
		
		button1.gameObject.SetActive (true);
		button2.gameObject.SetActive (true);
		button3.gameObject.SetActive (true);
		
	}
	
	void ClosePanel(){
		modalPanelObject.SetActive(false);
	}
}
