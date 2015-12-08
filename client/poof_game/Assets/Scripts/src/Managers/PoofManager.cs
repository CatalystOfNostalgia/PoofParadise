using UnityEngine;

/**
 * TODO: Insert description
 */
public class PoofManager : MonoBehaviour {

    public static PoofManager poofManager;
    /// Poof Attraction Rating is how much Poofs want to come to your land
    /// It goes up with decorative building
    /// It goes down with more poofs
    private int poofAttractionRating;

	/**
     * Use this for initialization
     */
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
        poofAttractionRating = 0;
    }

    /**
     * TODO: Insert description
     */
    public void beamDownPoof(int poofRate)
    {
        beamDownPoof(GameManager.gameManager.GetRandomSpawnPoint(), poofRate);
    }

    /**
     * TODO: Insert description
     */
    public void beamDownPoof(Tuple currentLocation, int poofRate)
    {
        poofAttractionRating += poofRate;
        if (SaveState.state.poofCount >= SaveState.state.poofLimit)
        {
            Debug.Log("[PoofManager] Too many poofs");
            return;
        }

        int poofsAvailable = SaveState.state.poofLimit - SaveState.state.poofCount;
        int poofsToSpawnCount = poofAttractionRating > poofsAvailable ? poofsAvailable : poofAttractionRating;
        for (int i = 0; i<poofsToSpawnCount; i++)
        {
            GameManager.gameManager.SpawnPoof(GameManager.gameManager.poofPrefab, currentLocation, new System.Collections.Generic.List<GameObject>());
            SaveState.state.poofCount++;
            poofAttractionRating--;
            Debug.Log("[PoofManager] Spawned a poof");
        }
    }
}
