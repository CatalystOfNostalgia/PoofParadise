using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

    public TileScript grid;
    public GameManager gManager;
    public SaveState saveState;

    private bool spawn = false;

	// Use this for initialization
	void Awake () {
        Instantiate(grid, new Vector3(0, 0, 15), Quaternion.identity);
        Instantiate(gManager, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(saveState, new Vector3(0, 1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
        if (!spawn)
        {
            GameManager.gameManager.SpawnPoofs();
        }
    }
}
