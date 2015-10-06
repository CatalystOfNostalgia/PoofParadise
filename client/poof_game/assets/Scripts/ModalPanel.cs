using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalPanel : MonoBehaviour {

	public Text question;
	public Image iconImage;
	public Button button1;
	public Button button2;
	public Button button3;
	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance(){
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;
			if(!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return modalPanel;
	}
	public void Choice(string question, UnityAction button1Event, UnityAction button2Event, UnityAction button3Event){
		//modal panel should be visible on screen
		modalPanelObject.SetActive (true);

		button1.onClick.RemoveAllListeners ();
		button1.onClick.AddListener (ClosePanel);
		button1.onClick.AddListener (() => BuildingManager.Instance().makeNewBuilding());


		button2.onClick.RemoveAllListeners ();
		button2.onClick.AddListener (button2Event);
		button2.onClick.AddListener (ClosePanel);

		
		button3.onClick.RemoveAllListeners ();
		button3.onClick.AddListener (button3Event);
		button3.onClick.AddListener (ClosePanel);

		this.question.text = question; //sets question in dialogue box
		this.iconImage.gameObject.SetActive (false);

		button1.gameObject.SetActive (true);
		button2.gameObject.SetActive (true);
		button3.gameObject.SetActive (true);

	}

	
	void ClosePanel(){
		modalPanelObject.SetActive(false);
	}
}
