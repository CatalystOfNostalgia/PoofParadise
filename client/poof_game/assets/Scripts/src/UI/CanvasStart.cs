using UnityEngine;
using UnityEngine.UI;

/**
 * Builds the canvas dynamically
 */
public class CanvasStart : MonoBehaviour {

	/**
     * Use to initialize all static singleton children
     *
     * This is necessary because nested panels that are
     * inactive never get a chance to run start
     */
	void Start () {

        BuildUI();

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

        GameObject buildingPanel = this.transform.Find("Building Panel(Clone)").gameObject;
        if (buildingPanel == null)
        {
            Debug.LogError("[CanvasStart] Building Panel is null");
            return;
        }

        if (BuildingPanel.buildingPanel == null)
        {
            DontDestroyOnLoad(modelPanel);
            BuildingPanel.buildingPanel = buildingPanel.GetComponent<BuildingPanel>();
        }

        else if (BuildingPanel.buildingPanel != buildingPanel)
        {
            Destroy(this);
        }

        GameObject poofCounterPanel = this.transform.Find("Poof Counter Panel(Clone)").gameObject;
        if (poofCounterPanel == null)
        {
            Debug.LogError("[CanvasStart] Poof Counter Panel is null");
            return;
        }

        if (PoofCounterPanel.poofCounterPanel == null)
        {
            DontDestroyOnLoad(modelPanel);
            PoofCounterPanel.poofCounterPanel = poofCounterPanel.GetComponent<PoofCounterPanel>();
        }

        else if (PoofCounterPanel.poofCounterPanel != poofCounterPanel)
        {
            Destroy(this);
        }

<<<<<<< HEAD
		GameObject upgradePanel = this.transform.Find("Upgrade Panel(Clone)").gameObject;
		if (upgradePanel == null)
		{
			Debug.LogError("[CanvasStart] Upgrade Panel is null");
			return;
		}
		
		if (UpgradePanel.upgradePanel == null)
		{
			DontDestroyOnLoad(modelPanel);
			UpgradePanel.upgradePanel = upgradePanel.GetComponent<UpgradePanel>();
		}
		
		else if (UpgradePanel.upgradePanel != upgradePanel)
		{
			Destroy(this);
		}
		GameObject destroyPanel = this.transform.Find("Destroy Panel(Clone)").gameObject;
		if (destroyPanel == null)
		{
			Debug.LogError("[CanvasStart] Upgrade Panel is null");
			return;
		}
		
		if (DestroyPanel.destroyPanel == null)
		{
			DontDestroyOnLoad(modelPanel);
			DestroyPanel.destroyPanel = destroyPanel.GetComponent<DestroyPanel>();
		}
		
		else if (DestroyPanel.destroyPanel != destroyPanel)
		{
			Destroy(this);
		}
=======
        GameObject shopPanel = this.transform.Find("Shop Panel(Clone)").gameObject;
        if(ShopPanel.shopPanel == null)
        {
            DontDestroyOnLoad(shopPanel);
            ShopPanel.shopPanel = shopPanel.GetComponent<ShopPanel>();
        }
        else if (ShopPanel.shopPanel != shopPanel)
        {
            Destroy(this);
        }
>>>>>>> d2fac759e835ea9263dda3998e7196c986bc16af
    }

    /**
     * Generates the UI from prefab panels
     */
    private void BuildUI()
    {
        foreach (CanvasRenderer cr in PrefabManager.prefabManager.panels)
        {
            CanvasRenderer temp = Instantiate(cr, cr.transform.position, Quaternion.identity) as CanvasRenderer;
            Text[] texts = temp.GetComponentsInChildren<Text>();
            foreach (Text t in texts)
            {
                t.font = (Font)Resources.Load("Font/Candara");
            }
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
