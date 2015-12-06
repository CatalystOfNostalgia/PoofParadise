using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

    public Manager manager { get; set; }

    private Manager[] managers;

    /**
     * Adds all essential game objects to scene
     */
    void Awake () {
        // Instantiates managers object
        manager = (Manager)Resources.Load("Prefabs/Managers/Managers", typeof(Manager));
        Instantiate(manager, new Vector3(0, 0, 15), Quaternion.identity);

        // Renders scene ones managers become active
        StartCoroutine("RenderScene");

        // Be careful! Anything after this coroutine will run 
        // before coroutine finishes

		InvokeRepeating ("autoSave", 2, 30F);
    }

	void autoSave () {
	
		SaveState.state.PushToServer();	
	}

	void OnApplicationQuit(){
		//SaveState.state.PushToServer();
		Debug.Log("Save ON QUITTING!!!");
	}

    /**
     * Use this function to build the scene
     * The coroutine allows us to let Unity 
     * run other tasks before running another
     * iteration of the loop
     */
    private IEnumerator RenderScene()
    {
        while (!SceneIsReady())
        {
            yield return null;
        }
    
        Debug.Log("scene is ready");
        // Build canvas
        Instantiate(PrefabManager.prefabManager.canvas, new Vector3(0, 0, 0), Quaternion.identity);

        // build and populate the game grid
        TileScript.grid.BuildGameGrid();
        
		try{
			SaveState.state.loadJSON(SceneState.state.userInfo);
		}
		catch(UnityException e){
			Debug.Log("[GameStart] User did not log in");
		}
        TileScript.grid.PopulateGameGrid();

        // Generate all poofs/elemari
        GameManager.gameManager.SpawnPoofs();
		Debug.Log ("scene is generated");
    }

    /**
     * Returns true only if all essential game objects
     * are built
     */
    private bool SceneIsReady()
    {
        BuildManagersList();
        foreach (Manager manager in managers)
        {
            if (manager == null)
            {
                return false;
            }
        }
        return true;
    }

    /**
     * Builds a list of managers for testing
     */
    private void BuildManagersList()
    {
        managers = new Manager[6];
        managers[0] = GameManager.gameManager;
        managers[1] = SaveState.state;
        managers[2] = TileScript.grid;
        managers[3] = BuildingManager.buildingManager;
        managers[4] = SoundManager.soundManager;
        managers[5] = PrefabManager.prefabManager;
    }

    /**
     * Runs a pull from server and populates the grid
     */
    public void TestJSON()
    {

		//SaveState.state.PullFromServer ("ted1", "password");
        TileScript.grid.PopulateGameGrid ();

		//testing saving game
		//SaveState.state.PushToServer ();


    }
}
