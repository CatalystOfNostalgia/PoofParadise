using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasStart : MonoBehaviour {

	/**
     * Use to initialize all static singleton children
     */
	void Start () {

		Button jsonButton = this.transform.FindChild ("Test JSON").gameObject.GetComponent<Button>();

		jsonButton.onClick.RemoveAllListeners ();
		jsonButton.onClick.AddListener (() => SaveState.state.PullFromServer()); 

        GameObject modelPanel = this.transform.FindChild("Model Panel").gameObject;

        if (ModelPanel.modelPanel == null)
        {
            DontDestroyOnLoad(modelPanel);
            ModelPanel.modelPanel = modelPanel.GetComponent<ModelPanel>();
        }

        else if (ModelPanel.modelPanel != modelPanel)
        {
            Destroy(this);
        }
	}

    /**
     * A sad attempt at making a singleton function
     */
    private void MakeSingleton<T>(GameObject obj)
    {
        T test = obj.GetComponent<T>();
    }
	
}
