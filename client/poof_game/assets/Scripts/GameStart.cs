using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

    public TileScript grid;
    public GameManager gManager;
    public SaveState saveState;

    private bool spawn = false;

	// Adds all essential game objects to scene
	void Awake () {
        Instantiate(grid, new Vector3(0, 0, 15), Quaternion.identity);
        Instantiate(gManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(saveState, new Vector3(0, 1, 0), Quaternion.identity);
        StartCoroutine("RenderScene");
    }

    /**
     * Use this function to build the scene
     * The coroutine allows us to let Unity 
     * run other tasks before running another
     * iteration of the loop
     */
    private IEnumerator RenderScene()
    {
        while (GameManager.gameManager == null)
        {
            yield return null;
        }
        GameManager.gameManager.SpawnPoofs();
    }

    // Update is called once per frame
    void Update () {
    }
}
