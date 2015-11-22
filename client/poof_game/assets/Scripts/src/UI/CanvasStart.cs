using UnityEngine;
using UnityEngine.UI;

public class CanvasStart : MonoBehaviour {

	/**
     * Use to initialize all static singleton children
     */
	void Start () {

        BuildUI();

		Button jsonButton = this.transform.FindChild ("Test Panel(Clone)/Test JSON").gameObject.GetComponent<Button>();
		
		jsonButton.onClick.RemoveAllListeners ();
		jsonButton.onClick.AddListener (() => SaveState.state.PullFromServer());

        GameObject modelPanel = this.transform.FindChild("Model Panel(Clone)").gameObject;

        if (ModelPanel.modelPanel == null)
        {
            DontDestroyOnLoad(modelPanel);
            ModelPanel.modelPanel = modelPanel.GetComponent<ModelPanel>();
        }

        else if (ModelPanel.modelPanel != modelPanel)
        {
            Destroy(this);
        }

        GameObject settingsMenu = this.transform.FindChild("Settings Panel(Clone)").gameObject;

        if (SettingsMenu.menu == null)
        {
            DontDestroyOnLoad(modelPanel);
            SettingsMenu.menu = settingsMenu.GetComponent<SettingsMenu>();
        }

        else if (SettingsMenu.menu != settingsMenu)
        {
            Destroy(this);
        }
        
		GameObject microMenu = this.transform.FindChild("Microtransaction Menu(Clone)").gameObject;
		
		if (MicrotransactionPanel.mp == null)
		{
			DontDestroyOnLoad(microMenu);
			MicrotransactionPanel.mp = microMenu.GetComponent<MicrotransactionPanel>();
		}
		
		else if (MicrotransactionPanel.mp != microMenu)
		{
			Destroy(this);
		}
    }

    /**
     * Generates the UI from prefab panels
     */
    private void BuildUI()
    {
        foreach (CanvasRenderer cr in PrefabManager.prefabManager.panels)
        {
            CanvasRenderer temp = Instantiate(cr, cr.transform.position, Quaternion.identity) as CanvasRenderer;
            temp.transform.SetParent(this.transform, false);
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
