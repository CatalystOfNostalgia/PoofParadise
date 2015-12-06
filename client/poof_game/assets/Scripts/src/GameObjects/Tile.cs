using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    // Location
    public Tuple index { get; set; }

    // A boolean to determine if this tile is vacant
    public bool isVacant { get; set; }

    // the building occupying this tile
    public Building building { get; set; }

    // Tile ID value
    public int id { get; set; }

    // References to nearby tiles for easy access
    public Tile upTile { get; set; }
    public Tile downTile { get; set; }
    public Tile leftTile { get; set; }
    public Tile rightTile { get; set; }
    public Tile upLeftTile { get; set; }
    public Tile upRightTile { get; set; }
    public Tile downLeftTile { get; set; }
    public Tile downRightTile { get; set; }

    // A private field for the color of this object
    private Color startColor;

    public Building PlaceBuilding(Building newbuilding) 
    {
        if (isVacant) {

            building = Instantiate (newbuilding, 
                                    new Vector3(this.transform.position.x, 
                                    this.transform.position.y - .325f, 
                                    this.transform.position.y - .325f
                                               ), 
                                    Quaternion.identity
                                    ) as Building;

            // set tiles to filled
            isVacant = false;
            if ( leftTile != null ) { leftTile.isVacant = false; }
            if ( downTile != null ) { downTile.isVacant = false; }
            if ( downLeftTile != null ) { downLeftTile.isVacant = false; }

            return building;
        }

        return null;
    }

    /**
     * A method that works with
     * the collider of this object
     */
    void OnMouseDown()
    {
        Debug.Log(index.ToString());
        foreach (Tile t in GetAdjacentTiles()) {
            //Debug.Log(t.id);
        }
    }

    /**
     * A method that works with
     * the collider of this object
     */
    void OnMouseEnter()
    {
        startColor = GetComponent<Renderer>().material.color;

        BuildingManager.buildingManager.selectedTile = this;

        if (isVacant)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        Debug.Log("enter tile: (" + index.x + ", " + index.y + ")");
    }

    /**
     * A method that works with
     * the collider of this object
     */
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;

        // if the new selected tile is already set the we don't want to set it to null
        if (BuildingManager.buildingManager.selectedTile == null || 
            this.index.Equals(BuildingManager.buildingManager.selectedTile.index)) {
           // do nothing 
        } else {
            BuildingManager.buildingManager.selectedTile = null;
        }

        Debug.Log("exit tile: (" + index.x + ", " + index.y + ")");
    }

    /**
     * Produces an array of all the tiles with 1 of this tile
     */
    public Tile[] GetAdjacentTiles()
    {
        List<Tile> nearbyTiles = new List<Tile>();
        AddTileToList(nearbyTiles, upTile);
        AddTileToList(nearbyTiles, downTile);
        AddTileToList(nearbyTiles, leftTile);
        AddTileToList(nearbyTiles, rightTile);
        AddTileToList(nearbyTiles, upLeftTile);
        AddTileToList(nearbyTiles, upRightTile);
        AddTileToList(nearbyTiles, downLeftTile);
        AddTileToList(nearbyTiles, downRightTile);
        return nearbyTiles.ToArray();
    }

    /**
     * A private helper method for adding a tile
     * to a list if it exists
     */
    private void AddTileToList(List<Tile> list, Tile tile)
    {
        if (tile != null)
        {
            list.Add(tile);
        }
    }
}
