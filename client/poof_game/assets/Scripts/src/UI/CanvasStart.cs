using UnityEngine;
using System.Collections;

public class CanvasStart : MonoBehaviour {

	/**
     * Use to initialize all static singleton children
     */
	void Start () {
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
