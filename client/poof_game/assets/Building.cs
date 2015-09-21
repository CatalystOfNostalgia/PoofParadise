using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    /**
     * For purposes of example, I am assuming a 10x10 grid, with (0,0) at the center. (0,0) is the center point; it itself is not a grid square.
     * I am assuming each gridline is 1 unit long.
     * 0 is not a valid coordinate. See:
        -3 -2 -1 | 1  2  3
     */
    bool selected;
    bool placed;
    int xCoord, yCoord; // Diagonal grid, measured from the center. X is the \ (diagonal left) axis, Y is the / (diagonal right) axis
    int size; // All buildings are square - this is determined by side size; e.g. a 3x3 building is size 3

    // Use this for initialization
    void Start()
    {
        selected = true;
        placed = false;
        size = 1;
        xCoord = 0;
        yCoord = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;

            xCoord = objPosition.x > 0 ? (int)(objPosition.x + 1) : (int)(objPosition.x - 1);
            yCoord = objPosition.y > 0 ? (int)(objPosition.y + 1) : (int)(objPosition.y - 1);
        }
        if (Input.GetMouseButtonUp(0))
        {
            selected = false;
            placed = true;

            //TODO legality check for legal coordinate values

            float gridX = xCoord > 0 ? (float)(xCoord - 0.5) : (float)(xCoord + 0.5);
            float gridY = xCoord > 0 ? (float)(yCoord - 0.5) : (float)(yCoord + 0.5);

            transform.position = new Vector3(gridX, gridY);
        }
    }
}
