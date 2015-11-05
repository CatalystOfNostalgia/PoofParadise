using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModelPanel : MonoBehaviour {

	public Text question;
	public Image iconImage;
	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button exit;

	public static ModelPanel modelPanel;

    void Start()
    {
        if (modelPanel == null)
        {
            DontDestroyOnLoad(gameObject);
            modelPanel = this;
        }
        else if (modelPanel != this)
        {
            Destroy(gameObject);
        }
    }

	public void Choice(string question, UnityAction button1Event, UnityAction button2Event, UnityAction button3Event, UnityAction button4Event){
		//modal panel should be visible on screen
		modelPanel.gameObject.SetActive (true);

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

		this.question.text = question; //sets question in dialogue box
		//this.iconImage.gameObject.SetActive (false);

		button1.gameObject.SetActive (true);
		button2.gameObject.SetActive (true);
		button3.gameObject.SetActive (true);

	}
	
	void ClosePanel(){
		modelPanel.gameObject.SetActive(false);
	}
}
