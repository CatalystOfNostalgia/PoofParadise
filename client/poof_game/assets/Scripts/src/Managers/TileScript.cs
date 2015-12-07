using UnityEngine;
using System.Collections.Generic;


/**
 * The TileScript is a static, one-of-a-kind singleton object that serves as the
 * game grid from which all other game functions are derived
 */

public class TileScript : Manager {

    // Singleton variable so that only one grid variable is active per scene
    public static TileScript grid;

    // Fields that are used to contain and maintain all the tiles
    public Tile[] tiles { get; set; }
	
	// Public fields
	public int gridX;
	public int gridY;
	
    /**
     * Initializes the list of tiles
     * Generates the game grid
     */
	override public void Start() {
		
        if (grid == null)
        {
            grid = this;
        }
        else if (grid != this)
        {
            Destroy(gameObject);
        }
        tiles = new Tile[gridX * gridY];
    }

    /**
     * The main game grid building method
     * All nested function calls are private
     * to avoid users generating a half baked
     * grid object
     */
    public void BuildGameGrid()
    {
		GenerateTiles(PrefabManager.prefabManager.tiles, PrefabManager.prefabManager.borders, transform.position, gridY, gridX);
        GiveNeighbors();
    }

	/**
	 * This will add all of the users buildings to the grid
	 */
	public void PopulateGameGrid()
	{
		if (SaveState.state.resourceBuildings.Count == 0 &&
            SaveState.state.decorativeBuildings.Count == 0 &&
            SaveState.state.residenceBuildings.Count == 0) {
			// Hopefully HQ building lv1 is at index 0
			SaveState.state.hq = PrefabManager.prefabManager.headQuarterBuildings[0];
		}
		foreach (KeyValuePair<Tuple, ResourceBuilding> entry in SaveState.state.resourceBuildings) 
		{
			BuildingManager.buildingManager.PlaceBuilding(entry.Value, GetTile (entry.Key));
		}
		foreach (KeyValuePair<Tuple, DecorativeBuilding> entry in SaveState.state.decorativeBuildings) 
		{
			BuildingManager.buildingManager.PlaceBuilding(entry.Value, GetTile (entry.Key));
		}
		foreach (KeyValuePair<Tuple, ResidenceBuilding> entry in SaveState.state.residenceBuildings) 
		{
			BuildingManager.buildingManager.PlaceBuilding(entry.Value, GetTile (entry.Key));
		}
	}

    /**
     * A method used for building the game grid
     */
	private void GenerateTiles(Tile[] tile, GameObject[] borders, Vector3 orig, int width, int height)
    {
        // Create a grid object to attach all of the tiles
        GameObject grid = new GameObject();
        grid.name = "Grid";

        // Builds the tile grid
        int tilesGenerated = 0;
        for (int i = 0; i < gridY; i++)
        {
            for (int j = 0; j < gridX; j++)
            {
                Vector3 location = orig + new Vector3(0.55f*(i + j - 5), .32f*(j - i), -2);
                Tile myTile = Instantiate(tile[(i + j) % tile.Length], location, Quaternion.identity) as Tile;
                myTile.transform.parent = grid.transform; // Make tile a child of the grid object
                myTile.index = new Tuple(i, j);
                myTile.id = tilesGenerated;
				myTile.isVacant = true;
                tiles[tilesGenerated] = myTile;
                tilesGenerated++;
            }
        }

		// All these magic numbers have to do with making sure the border is the correct distance away from the tiles. 
		int borderGenerated = 0;
		for (int i = 0; i < gridY*0.8; i++)
		{
			for (int j = 0; j < gridX*0.8; j++)
			{
				bool clear = true;
				Vector3 location = orig + new Vector3(2.56f*(i + j - 9), 1.7f*(j - i), -2);
				foreach (Tile t in tiles){
					if( Mathf.Abs(t.transform.position.x - location.x )<1 & Mathf.Abs(t.transform.position.y - location.y)<2.2  ){
						clear = false;
					}
				}
				if (clear){
					GameObject myTile = Instantiate(borders[(i + j) % borders.Length], location, Quaternion.identity) as GameObject;
					myTile.transform.parent = grid.transform; // Make tile a child of the grid object

					borderGenerated++;
				}
			}
		}


    }

    /**
     * Gives each tile references to their neighbors
     */
    public void GiveNeighbors()
    {
        foreach (Tile t in tiles)
        {
            // Assigns the upper tile
            if (TileExistsAt(t.id - gridX))
            {
                t.upTile = tiles[t.id - gridX];
            }
            // Assigns the lower tile
            if (TileExistsAt(t.id + gridX))
            {
                t.downTile = tiles[t.id + gridX];
            }
            // Assigns the left tile
            if (TileExistsAt(t.id - 1) && (t.id % gridX != 0))
            {
                t.leftTile = tiles[t.id - 1];
            }
            // Assigns the right tile
            if (TileExistsAt(t.id + 1) && ((t.id + 1) % gridX != 0))
            {
                t.rightTile = tiles[t.id + 1];
            }
            // Assigns the upper left tile
            if (TileExistsAt(t.id - gridX - 1) && (t.id % gridX != 0))
            {
                t.upLeftTile = tiles[t.id - gridX - 1];
            }
            // Assigns the upper right tile
            if (TileExistsAt(t.id - gridX + 1) && ((t.id + 1) % gridX != 0))
            {
                t.upRightTile = tiles[t.id - gridX + 1];
            }
            // Assigns the lower left tile
            if (TileExistsAt(t.id + gridX - 1) && (t.id % gridX != 0))
            {
                t.downLeftTile = tiles[t.id + gridX - 1];
            }
            // Assigns the lower right tile
            if (TileExistsAt(t.id + gridX + 1) && ((t.id + 1) % gridX != 0))
            {
                t.downRightTile = tiles[t.id + gridX + 1];
            }
        }
    }

    /**
     * A helper method for determining if
     * a tile exists at the target id
     */
    private bool TileExistsAt(int targetID)
    {
        if (targetID >= 0 && targetID < tiles.Length)
        {
            return true;
        }
        return false;
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
        if (start.x < gridX - 1)
        {
            possiblePaths.Add(new Tuple(start.x + 1, start.y));
        }
        // Add the left path
        if (start.x > 0)
        {
            possiblePaths.Add(new Tuple(start.x - 1, start.y));
        }
        // Add the top path
        if (start.y < gridY - 1)
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
}


