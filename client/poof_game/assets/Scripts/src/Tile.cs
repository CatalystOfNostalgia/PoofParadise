using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    // Location
    public Tuple index { get; set; }

    // A boolean to determine if this tile is vacant
    public bool isVacant { get; set; }

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

    private Color startColor;

    void Start()
    {
        isVacant = true;
    }

    void OnMouseDown()
    {
        Debug.Log(index.ToString());
    }

    void OnMouseEnter()
    {
        startColor = GetComponent<Renderer>().material.color;
        if (isVacant)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }
}
