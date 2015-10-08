using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Store all of the prefabs here
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject airPrefab;
    public GameObject earthPrefab;

    // Store private list of Game Objects
    private List<GameObject> fireActive;
    private List<GameObject> waterActive;
    private List<GameObject> earthActive;
    private List<GameObject> airActive;

    void Start()
    {
        fireActive = new List<GameObject>();
        waterActive = new List<GameObject>();
        earthActive = new List<GameObject>();
        airActive = new List<GameObject>();
    }

    /**
     * Spawns all of the poofs at one location
     * on game start
     */
    public void SpawnPoofs()
    {
        int fire = SaveState.state.fireEle;
        int water = SaveState.state.waterEle;
        int earth = SaveState.state.earthEle;
        int air = SaveState.state.airEle;

        // Fire loop
        if (fireActive.Count < fire && fire > 0)
        {
            for (int i = 0; i < fire; i++)
            {
                SpawnPoof(firePrefab, GetRandomSpawnPoint(), fireActive);
                fire--;
            }
        }

        // Water loop
        if (waterActive.Count < water && water > 0)
        {
            for (int i = 0; i < water; i++)
            {
                SpawnPoof(waterPrefab, GetRandomSpawnPoint(), waterActive);
                water--;
            }
        }

        // Earth loop
        if (earthActive.Count < earth && earth > 0)
        {
            for (int i = 0; i < earth; i++)
            {
                SpawnPoof(earthPrefab, GetRandomSpawnPoint(), earthActive);
                earth--;
            }
        }

        // Air loop
        if (airActive.Count < air && air > 0)
        {
            for (int i = 0; i < air; i++)
            {
                SpawnPoof(airPrefab, GetRandomSpawnPoint(), airActive);
                air--;
            }
        }
    }

    /**
     * Allows the caller to spawn a poof
     */
    public void SpawnPoof(GameObject prefab, Tuple spawnPoint, List<GameObject> active)
    {
        Vector3 position = GetSpawnVector(spawnPoint);
        GameObject go = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        active.Add(go);
        CharacterScript cs = go.GetComponent<CharacterScript>();
        cs.onTile = TileScript.grid.GetTile(spawnPoint).gameObject;
    }

    /**
     * Generates a random tuple for spawning poofs
     */
    public Tuple GetRandomSpawnPoint()
    {
        Tile[] allTiles = TileScript.grid.tiles.ToArray();
        return allTiles[(int)Random.Range(0, allTiles.Length - 1)].index;
    }

    /**
     * Returns a Vector3 associated with this grid position
     */
    public Vector3 GetSpawnVector(Tuple spawnPoint)
    {
        return TileScript.grid.GetTile(spawnPoint).transform.position;
    }
}
