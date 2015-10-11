﻿using UnityEngine;
using System.Collections.Generic;

/**
 * The TileScript is a static, one-of-a-kind singleton object that serves as the
 * game grid from which all other game functions are derived
 */

public class TileScript : MonoBehaviour {

    // Singleton variable so that only one grid variable is active per scene
    public static TileScript grid;

    // Fields that are used to contain and maintain all the tiles
    public Tile[] tiles { get; set; }
	
	// Public fields
	public int gridX;
	public int gridY;
    public Tile[] prefabs;
	
    /**
     * Initializes the list of tiles
     * Generates the game grid
     */
	void Start() {
		
        if (grid == null)
        {
            grid = this;
        }
        else if (grid != this)
        {
            Destroy(gameObject);
        }
        tiles = new Tile[gridX * gridY];
        BuildGameGrid();
    }

    /**
     * The main game grid building method
     * All nested function calls are private
     * to avoid users generating a half baked
     * grid object
     */
    public void BuildGameGrid()
    {
        GenerateTiles(prefabs, transform.position, gridY, gridX);
        GiveNeighbors();
    }

    /**
     * A method used for building the game grid
     */
    private void GenerateTiles(Tile[] tile, Vector3 orig, int width, int height)
    {
        int tilesGenerated = 0;
        // Builds the tile grid
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Vector3 location = orig + new Vector3(1.10f*(i + j - 5), .64f*(j - i), -2);
                Tile myTile = Instantiate(tile[(i + j) % tile.Length], location, Quaternion.identity) as Tile;
                myTile.transform.parent = this.transform; // Make tile a child of the grid
                myTile.index = new Tuple(i, j);
                myTile.id = tilesGenerated;
                tiles[tilesGenerated] = myTile;
                tilesGenerated++;
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
            if (TileExistsAt(t.id - 1))
            {
                t.leftTile = tiles[t.id - 1];
            }
            // Assigns the right tile
            if (TileExistsAt(t.id + 1))
            {
                t.rightTile = tiles[t.id + 1];
            }
            // Assigns the upper left tile
            if (TileExistsAt(t.id - gridX - 1))
            {
                t.upLeftTile = tiles[t.id - gridX - 1];
            }
            // Assigns the upper right tile
            if (TileExistsAt(t.id - gridX + 1))
            {
                t.upRightTile = tiles[t.id - gridX + 1];
            }
            // Assigns the lower left tile
            if (TileExistsAt(t.id + gridX - 1))
            {
                t.upRightTile = tiles[t.id + gridX - 1];
            }
            // Assigns the lower right tile
            if (TileExistsAt(t.id + gridX + 1))
            {
                t.upRightTile = tiles[t.id + gridX + 1];
            }
        }
    }

    /**
     * A helper method for determining if
     * a tile exists at the target id
     */
    private bool TileExistsAt(int targetID)
    {
        if (targetID > 0 && targetID < tiles.Length)
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

