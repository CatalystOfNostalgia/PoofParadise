using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    public GameObject manager;

    // Adds all essential game objects to scene
    void Awake () {
        Instantiate(manager, new Vector3(0, 0, 15), Quaternion.identity);
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

        TileScript.grid.BuildGameGrid ();
        GameManager.gameManager.SpawnPoofs();
        SetUpMusicSettings();
    }

    /**
     * Returns true only if all essential game objects
     * are built
     */
    private bool SceneIsReady()
    {
        // Too lazy to clean this up atm
        if (GameManager.gameManager == null || SaveState.state == null || TileScript.grid == null || BuildingManager.manager == null || SoundManager.soundManager == null)
        {
            return false;
        }
        return true;
    }

    public void TestJSON()
    {
        SaveState.state.PullFromServer ();
        TileScript.grid.PopulateGameGrid ();
    }

    public void SetUpMusicSettings()
    {
        var button = GameObject.Find("Next Song");
        Button nextSong = button.GetComponent<Button>();
        nextSong.onClick.AddListener(SoundManager.soundManager.nextSong);
    }
}
