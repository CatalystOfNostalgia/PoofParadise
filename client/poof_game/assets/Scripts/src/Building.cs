using UnityEngine;
using System.Collections;
using System;


public abstract class Building : MonoBehaviour {
    private bool selected { get; set; }
    private bool placed { get; set; }
    public int xCoord { get; set;  } 
    public int yCoord { get; set; }  // Diagonal grid, measured from the far left corner. X is the \ (diagonal left) axis, Y is the / (diagonal right) axis
    public int size { get; set; } // All buildings are square - this is determined by side size; e.g. a 3x3 building is size 3

    // Use this for initialization
    protected virtual void Start()
    {
        selected = true;
        placed = false;
        size = 1;
        xCoord = 0;
        yCoord = 0;
    }

    // Update is called once per frame
//    void Update()
//    {
//        if (selected)
//        {
//            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
//            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
//            transform.position = objPosition;
//
//
//            RaycastHit hit;
//            Ray hoverRay = new Ray(objPosition, new Vector3(0, 0, -1));
//
//            if (Physics.Raycast(hoverRay, out hit))
//            {
//                if(hit.collider.tag == "Tile")
//                {
//                    int numLength = ((hit.collider.name.Length - 8) / 2) + 1;
//                    int.TryParse(hit.collider.name.Substring(5, numLength), out xCoord);
//                    int.TryParse(hit.collider.name.Substring(5 + numLength + 1, numLength), out yCoord);
//                }
//                else
//                {
//                    xCoord = -5;
//                    yCoord = -5;
//                }
//            }
//            else
//            {
//                xCoord = -5;
//                yCoord = -5;
//            }
//
//            /*xCoord = objPosition.x > 0 ? (int)(objPosition.x + 1) : (int)(objPosition.x - 1);
//            yCoord = objPosition.y > 0 ? (int)(objPosition.y + 1) : (int)(objPosition.y - 1);*/
//        }
//        if (Input.GetMouseButtonUp(0))
//        {
//            selected = false;
//            placed = true;
//
//            //TODO: legality check for legal coordinate values
//
//            /*if(xCoord < 0 && yCoord < 0) //If mouse button released on invalid coords, destroys object
//            {
//                //note that this uses the calculated coords, not the actual coords
//                Destroy(gameObject);
//            }
//            else
//            {*/
//                float gridX = (float)((xCoord * 1.125 - 5.625) + (yCoord * 1.125));
//                float gridY = (float)((xCoord * -0.65) + (yCoord * 0.65));
//
//
//                transform.position = new Vector3(gridX, gridY);
//            //}
//        }
//    }
}
