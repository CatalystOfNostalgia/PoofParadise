using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    public GameObject manager;
    public Canvas canvas;

    private Manager[] managers;

    /**
     * Adds all essential game objects to scene
     */
    void Awake () {
        // Instantiates managers object along with the game canvas
        Instantiate(manager, new Vector3(0, 0, 15), Quaternion.identity);
        Instantiate(canvas, new Vector3(0, 0, 0), Quaternion.identity);

        // Renders scene ones managers become active
        StartCoroutine("RenderScene");

        // Be careful! Anything after this coroutine will run 
        // before coroutine finishes
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

        // build and populate the game grid
        TileScript.grid.BuildGameGrid();

        // Generate all poofs/elemari
        GameManager.gameManager.SpawnPoofs();
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
        managers = new Manager[5];
        managers[0] = GameManager.gameManager;
        managers[1] = SaveState.state;
        managers[2] = TileScript.grid;
        managers[3] = BuildingManager.buildingManager;
        managers[4] = SoundManager.soundManager;
    }

    /**
     * Runs a pull from server and populates the grid
     */
    public void TestJSON()
    {
        SaveState.state.PullFromServer ();
        TileScript.grid.PopulateGameGrid ();
    }
}
