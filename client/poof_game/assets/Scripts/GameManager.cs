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
    public void SpawnPoofs(Vector3 position)
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
                GameObject go = Instantiate(firePrefab, position, Quaternion.identity) as GameObject;
                fireActive.Add(go);
                CharacterScript cs = go.GetComponent<CharacterScript>();
                cs.grid = TileScript.grid.gameObject;
                cs.onTile = TileScript.grid.GetTile(new Tuple(0, 0)).gameObject;
                fire--;
            }
        }

        // Water loop
        if (waterActive.Count < water && water > 0)
        {
            for (int i = 0; i < water; i++)
            {
                GameObject go = Instantiate(waterPrefab, position, Quaternion.identity) as GameObject;
                waterActive.Add(go);
                CharacterScript cs = go.GetComponent<CharacterScript>();
                cs.grid = TileScript.grid.gameObject;
                cs.onTile = TileScript.grid.GetTile(new Tuple(0, 0)).gameObject;
                water--;
            }
        }

        // Earth loop
        if (earthActive.Count < earth && earth > 0)
        {
            for (int i = 0; i < earth; i++)
            {
                GameObject go = Instantiate(earthPrefab, position, Quaternion.identity) as GameObject;
                earthActive.Add(go);
                CharacterScript cs = go.GetComponent<CharacterScript>();
                cs.grid = TileScript.grid.gameObject;
                cs.onTile = TileScript.grid.GetTile(new Tuple(0, 0)).gameObject;
                earth--;
            }
        }

        // Air loop
        if (airActive.Count < air && air > 0)
        {
            for (int i = 0; i < air; i++)
            {
                GameObject go = Instantiate(airPrefab, position, Quaternion.identity) as GameObject;
                airActive.Add(go);
                CharacterScript cs = go.GetComponent<CharacterScript>();
                cs.grid = TileScript.grid.gameObject;
                cs.onTile = TileScript.grid.GetTile(new Tuple(0, 0)).gameObject;
                air--;
            }
        }
    }
}
