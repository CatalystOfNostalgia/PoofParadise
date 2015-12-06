using UnityEngine;
using System.Collections.Generic;

/**
 * The GameManager script is responsible
 * for managing the game state
 */
public class GameManager : Manager {

    public static GameManager gameManager;

    // Store all of the prefabs here
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject airPrefab;
    public GameObject earthPrefab;
    public GameObject poofPrefab;

    // Store private list of Game Objects
    private List<GameObject> fireActive;
    private List<GameObject> waterActive;
    private List<GameObject> earthActive;
    private List<GameObject> airActive;
    private List<GameObject> poofActive;

    private GameObject nest;

    /**
     * Converts GameManager to a singleton
     * Initializes poof/elemari lists
     */
    override public void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
        fireActive = new List<GameObject>();
        waterActive = new List<GameObject>();
        earthActive = new List<GameObject>();
        airActive = new List<GameObject>();
        poofActive = new List<GameObject>();
    }

    /**
     * Spawns all of the poofs at one location
     * on game start
     */
    public void SpawnPoofs()
    {
        int fireTotal = SaveState.state.fireEle;
        int waterTotal = SaveState.state.waterEle;
        int earthTotal = SaveState.state.earthEle;
        int airTotal = SaveState.state.airEle;
        int poofTotal = SaveState.state.poofCount;
        
        // calculate the total poofs
        CalculatePoofs();

        int fireLeft = fireTotal;
        int waterLeft = waterTotal;
        int earthLeft = earthTotal;
        int airLeft = airTotal;
        int poofLeft = poofTotal;

        nest = new GameObject();
        nest.name = "Character Nest";

        // Fire loop
        if (fireActive.Count < fireTotal && fireLeft > 0)
        {
            for (int i = 0; i < fireTotal; i++)
            {
                SpawnCharacter(firePrefab, GetRandomSpawnPoint(), fireActive);
                fireLeft--;
            }
        }

        // Water loop
        if (waterActive.Count < waterTotal && waterLeft > 0)
        {
            for (int i = 0; i < waterTotal; i++)
            {
                SpawnCharacter(waterPrefab, GetRandomSpawnPoint(), waterActive);
                waterLeft--;
            }
        }

        // Earth loop
        if (earthActive.Count < earthTotal && earthLeft > 0)
        {
            for (int i = 0; i < earthTotal; i++)
            {
                SpawnCharacter(earthPrefab, GetRandomSpawnPoint(), earthActive);
                earthLeft--;
            }
        }

        // Air loop
        if (airActive.Count < airTotal && airLeft > 0)
        {
            for (int i = 0; i < airTotal; i++)
            {
                SpawnCharacter(airPrefab, GetRandomSpawnPoint(), airActive);
                airLeft--;
            }
        }
        
        // Poof loop
        if (poofActive.Count < poofTotal && poofLeft > 0) 
        {
        	for (int i = 0; i < poofTotal; i++)
        	{
        		SpawnPoof(poofPrefab, GetRandomSpawnPoint(), poofActive);
        		poofLeft--;
        	}
        }
    }

    /**
     * determines the total poofs and poofs generated
     * returns a tuple with the x value being max number of poofs
     * and the y value is the generated poofs
     */
    public Tuple CalculatePoofs() 
    {

        int maxPoofs = 0;
        int generatedPoofs = 0;

        foreach ( KeyValuePair<Tuple, Building> entry in SaveState.state.buildings) {
            
            if (entry.Value.GetType() == typeof(DecorativeBuilding)) {
                DecorativeBuilding decBuilding = (DecorativeBuilding)entry.Value;
                generatedPoofs += decBuilding.generatedPoofs;
            }

            if (entry.Value.GetType() == typeof(ResidenceBuilding)) {
                ResidenceBuilding resBuilding = (ResidenceBuilding)entry.Value;
                maxPoofs += resBuilding.poofsAllowed;
            }

        }

        if (generatedPoofs > maxPoofs) { generatedPoofs = maxPoofs; }

        return new Tuple (maxPoofs, generatedPoofs);
    }

    /**
     * Allows the caller to spawn an elemari
     */
    public void SpawnCharacter(GameObject prefab, Tuple spawnPoint, List<GameObject> active)
    {
        Vector3 position = GetSpawnVector(spawnPoint);
        position.z = position.z - 1;
        GameObject go = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        active.Add(go);
        CharacterScript cs = go.GetComponent<CharacterScript>();
        cs.onTile = TileScript.grid.GetTile(spawnPoint);
        go.GetComponent<MovementScript>().initializeCharacter();
        go.transform.SetParent(nest.transform);
    }
    
    /**
     * Allows the caller to spawn a poof
     */
    public void SpawnPoof(GameObject prefab, Tuple spawnPoint, List<GameObject> active)
    {
		Vector3 position = GetSpawnVector(spawnPoint);
        position.z = position.z - 1;
        GameObject go = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		active.Add(go);
		PoofScript ps = go.GetComponent<PoofScript>();
		ps.onTile = TileScript.grid.GetTile(spawnPoint);
		go.GetComponent<MovementScript>().initializePoof();
        go.transform.SetParent(nest.transform);
    }

    /**
     * Generates a random tuple for spawning poofs
     */
    public Tuple GetRandomSpawnPoint()
    {
        return TileScript.grid.tiles[(int)Random.Range(0, TileScript.grid.tiles.Length - 1)].index;
    }

    /**
     * Returns a Vector3 associated with this grid position
     */
    public Vector3 GetSpawnVector(Tuple spawnPoint)
    {
        return TileScript.grid.GetTile(spawnPoint).transform.position;
    }
}
