using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Game grid
    public GameObject grid;

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
        int total = fire + water + earth + air;

        if (fireActive.Count < fire)
        {

            for (int i = 0; i < total; i++)
            {
                if (fire > 0)
                {
                    GameObject go = Instantiate(firePrefab, position, Quaternion.identity) as GameObject;
                    fireActive.Add(go);
                    CharacterScript cs = go.GetComponent<CharacterScript>();
                    cs.grid = this.grid;
                    TileScript ts = grid.GetComponent<TileScript>();
                    cs.onTile = ts.GetTile(new Tuple(0, 0)).gameObject;
                    fire--;
                }
                else if (water > 0)
                {
                    GameObject go = Instantiate(waterPrefab, position, Quaternion.identity) as GameObject;
                    waterActive.Add(go);
                    water--;
                }
                else if (earth > 0)
                {
                    GameObject go = Instantiate(earthPrefab, position, Quaternion.identity) as GameObject;
                    earthActive.Add(go);
                    earth--;
                }
                else if (air > 0)
                {
                    GameObject go = Instantiate(airPrefab, position, Quaternion.identity) as GameObject;
                    airActive.Add(go);
                    air--;
                }
            }
        }
    }
}
