using UnityEngine;
using System.Collections;

/* TileScript - Script attached to the Grid object in game; contains fields for to contain the
 * tiles, currently tiles are initialized by searching for GameObjects tagged as "Tile". Tiles 
 * are essentially hashed into an array by name, mimicing the current naming process within the editor. */

public class TileScript : MonoBehaviour {
	
	// Fields that are used to contain and maintain all the tiles
	private ArrayList tileObjects;
	private GameObject[,] tiles;
	
	// Constants defined for array operations
	public int mapArea = 36;
	public int mapLength = 6;
	public int mapWidth = 6;
	
	void Start() {
	
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
		}
	}
	
	// Public method that when given a tile returns an array of the 8 surrounding tiles (including the input tile)
	//		because the input tile is included, in most situations you should ignore index 4 in the array; it'll be null anyways
	public GameObject[] getAdjacentTiles (GameObject tile) {
	
		Tuple inputTile = hashTile(tile);
		GameObject[] toReturn = new GameObject[9];
		
		int index = 0;
		for (int j = inputTile.getY() - 1; j <= inputTile.getY() + 1; j++) {
			for (int i = inputTile.getX() - 1; i <= inputTile.getX() + 1; i++) {
				if (i < 0 || j < 0 || i > 5 || j > 5 || (i == inputTile.getX () && j == inputTile.getY())){
					index++;
				}
				else {
					toReturn.SetValue(tiles[i,j], index);
					index++;
				}	
		}}
		
		return toReturn;
	}
	
	// Method that when given an input tile, "hashes" the tile using its name, and returns a tuple
	//		which corresponds to the (x,y) coordinate of the tile in the 2D array (as well as in game)
	public static Tuple hashTile(GameObject tile) {

		if (tile.name.IndexOf("T") != 0) {
			return new Tuple(-1, -1);
		} else {
			return new Tuple(int.Parse(tile.name.Split(' ')[1].ToString()), int.Parse(tile.name.Split(' ')[2].ToString()));
		}
	}
}
