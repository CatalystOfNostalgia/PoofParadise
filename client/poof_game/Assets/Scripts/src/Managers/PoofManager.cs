using UnityEngine;
using System.Collections;

public class PoofManager : MonoBehaviour {

    public static PoofManager poofManager;

	// Use this for initialization
	void Start () {
        if (poofManager == null)
        {
            DontDestroyOnLoad(gameObject);
            poofManager = this;
        }
        else if (poofManager != this)
        {
            Destroy(gameObject);
        }
    }

    public void beamDownPoof(int poofRate)
    {
        beamDownPoof(GameManager.gameManager.GetRandomSpawnPoint(), poofRate);
    }

    public void beamDownPoof(Tuple currentLocation, int poofRate)
    {
        //need this check or poofsToSpawnCount might become greater than poofRate (e.g. 2- (-3) = 5)
        if (SaveState.state.poofCount + poofRate >= SaveState.state.poofLimit)
        {
            Debug.Log("[PoofManager] Too many poofs");
            return;
        }
        int poofsAvailable = SaveState.state.poofLimit - SaveState.state.poofCount;
        int poofsToSpawnCount = poofRate > poofsAvailable ? poofRate-poofsAvailable : poofRate;
        for (int i = 0; i<poofsToSpawnCount; i++)
        {
            GameManager.gameManager.SpawnPoof(GameManager.gameManager.poofPrefab, currentLocation, new System.Collections.Generic.List<GameObject>());
            Debug.Log("[PoofManager] Spawned a poof");
        }
    }
}
