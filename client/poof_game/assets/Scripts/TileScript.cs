using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* TileScript - Script attached to the Grid object in game; contains fields for to contain the
 * tiles, currently tiles are initialized by searching for GameObjects tagged as "Tile". Tiles 
 * are essentially hashed into an array by name, mimicing the current naming process within the editor. */

public class TileScript : MonoBehaviour {

    // Singleton variable so that only one grid variable is active per scene
    public static TileScript grid;


    // Fields that are used to contain and maintain all the tiles
    public List<Tile> tiles { get; set; }
	
	// Public fields
	public int mapLength = 6;
	public int mapWidth = 6;
    public GameManager manager;
    public GameObject[] prefab; // List of tile prefabs
	
    /**
     * Initializes the list of tiles
     * Generates the game grid
     */
	void Start() {

        if (grid == null)
        {
            //DontDestroyOnLoad(gameObject);
            grid = this;
        }
        else if (grid != this)
        {
            Destroy(gameObject);
        }

        tiles = new List<Tile>();
        Generate(prefab, transform.position, mapWidth, mapLength);
        // Load save from server
        // SaveState.state.Load() -> Local save line for demo?
        // Render scene with load data
        manager.SpawnPoofs(GetTile(new Tuple(0,0)).transform.position);
	}

    void Update()
    {
        manager.SpawnPoofs(GetTile(new Tuple(0, 0)).transform.position);
    }

    /**
     * A method used for building the game grid
     */
    public void Generate(GameObject[] tile, Vector3 orig, int width, int height)
    {
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                Vector3 location = orig + new Vector3(1.10f*(i + j - 5), .64f*(j - i), -2);
                GameObject gameObject = Instantiate(tile[(i + j) % tile.Length], location, Quaternion.identity) as GameObject;
                gameObject.transform.parent = this.transform;
                Tile t = gameObject.GetComponent<Tile>();
                t.index = new Tuple(i, j);
                tiles.Add(t);
            }
        }
    }
    
    /**
     * Generates a list of tiles adjacent to the
     * input tile
     */
    public List<Tile> GetAdjacentTiles(Tile tile)
    {
        List<Tile> listOfTiles = new List<Tile>();
        Tuple refer = tile.index;

        foreach (Tile test in tiles)
        {
            int x = refer.x - test.index.x;
            int y = refer.y - test.index.y;
            
            if (x <= 1 && x >= -1 && y <= 1 && y >= -1)
            {
                listOfTiles.Add(test);
            }
        }

        return listOfTiles;
    }

    /**
     * Given a tuple, this method will be able to produce
     * a list of legal tuples within a distance 1 from the start tuple
     *
     * This method takes advantage of the fact that tiles are indexed
     * by a tuple and treats that tuple as one of the corners of the tile
     * rather than the center
     */
    public List<Tuple> GetPossiblePaths(Tuple start)
    {
        List<Tuple> possiblePaths = new List<Tuple>();

        // Add the right path
        if (start.x < mapWidth)
        {
            possiblePaths.Add(new Tuple(start.x + 1, start.y));
        }
        // Add the left path
        if (start.x > 0)
        {
            possiblePaths.Add(new Tuple(start.x - 1, start.y));
        }
        // Add the top path
        if (start.y < mapLength)
        {
            possiblePaths.Add(new Tuple(start.x, start.y + 1));
        }
        // Add the bottom path
        if (start.y > 0)
        {
            possiblePaths.Add(new Tuple(start.x, start.y - 1));
        }

        return possiblePaths;
    }

    /**
     * A quick method for retrieving a tile based
     * on a Tuple 
     */
    public Tile GetTile(Tuple index)
    {
        foreach(Tile test in tiles)
        {
            // Implement equals in tuple
            if (test.index.Equals(index))
            {
                return test;
            }
        }
        return null;
    }

    /*
// Public method that when given a tile returns an array of the 8 surrounding tiles (including the input tile)
//		because the input tile is included, in most situations you should ignore index 4 in the array; it'll be null anyways
public GameObject[] getAdjacentTiles (GameObject tile) {

    Tuple inputTile = hashTile(tile);
    GameObject[] toReturn = new GameObject[9];

    int index = 0;
    for (int j = inputTile.y - 1; j <= inputTile.y + 1; j++) {
        for (int i = inputTile.x - 1; i <= inputTile.x + 1; i++) {
            if (i < 0 || j < 0 || i > 5 || j > 5 || (i == inputTile.x && j == inputTile.x)){
                index++;
            }
            else {
                toReturn.SetValue(tiles[i,j], index);
                index++;
            }	
    }}

    return toReturn;
}*/

    /*
// Method that when given an input tile, "hashes" the tile using its name, and returns a tuple
//		which corresponds to the (x,y) coordinate of the tile in the 2D array (as well as in game)
public static Tuple hashTile(GameObject tile) {

    if (tile.name.IndexOf("T") != 0) {
        return new Tuple(-1, -1);
    } else {
        return new Tuple(int.Parse(tile.name.Split(' ')[1].ToString()), int.Parse(tile.name.Split(' ')[2].ToString()));
    }
}*/

    /* ORIGINAL START CODE
		ArrayList namesOfTiles = new ArrayList();

		tileObjects = new ArrayList(GameObject.FindGameObjectsWithTag("Tile"));
		tiles = new GameObject[mapLength,mapWidth];
        
		int aisleCount = 0;
		
		foreach (GameObject t in tileObjects) {
			namesOfTiles.Add(t.name);
		}
		// I don't know why aisleCount is my other loop index, but it is
		//	this loop adds all of the found tiles into the 2D array
		while (aisleCount < mapWidth) {
			for (int i = 0; i < mapLength; i++) {
				tiles[i, aisleCount] = (GameObject)tileObjects[namesOfTiles.IndexOf("Tile " + i + " " + aisleCount)];
			}
			aisleCount++;
		}*/
}


