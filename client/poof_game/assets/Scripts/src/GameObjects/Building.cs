using UnityEngine;
using System.Collections;
using System;

public abstract class Building : MonoBehaviour {

    private bool selected { get; set; }
    private bool placed { get; set; }
    public bool created { get; set; }

    // Tracks the state of the building as a button
    public int ID { get; set; }

    // All buildings are square - this is determined by side size; e.g. a 3x3 building is size 3
    public int size { get; set; }

    // Building name
    public string buildingName { get; set; }

    // Building costs
    public int fireCost { get; set; }
    public int waterCost { get; set; }
    public int earthCost { get; set; }
    public int airCost { get; set; }

    // Use this for initialization
    protected virtual void Start()
    {
        //gameObject.AddComponent<ButtonDragScript>();
        gameObject.AddComponent<BoxCollider2D>();


//		Canvas canvas = new Canvas();
//		canvas.transform.parent = this.transform;
//
//		BuildingOptionPanel panel = Instantiate (new BuildingOptionPanel());
//		panel.transform.SetParent(canvas.transform);

        created = false;
        selected = true;
        placed = false;
        size = 1;
    }

    /**
     * On click functionality for building
     */
    void OnMouseDown()
    {
        GetComponent<Renderer>().material.color = Color.red;
		/// bring up the building option panel
		/// which includes 1. move building. 2. upgrade building. 3. remove building. 4. info on leaf
		/// 
		/// 

		//
    }

    void OnMouseDrag()
    {
        Vector3 loc = BuildingManager.buildingManager.selectedTile.transform.position;
        this.transform.position = new Vector3(loc.x, loc.y, loc.z - 1);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
