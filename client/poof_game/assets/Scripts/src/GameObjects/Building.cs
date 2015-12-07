using UnityEngine;
using System.Collections;
using System;

public abstract class Building : MonoBehaviour {

    private bool selected { get; set; }
    private bool placed { get; set; }
    public bool created;

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

    // Interface flags
    public bool canDrag { get; set; }
    public bool showOptions { get; set; }

    private Canvas options;

    // Use this for initialization
    protected virtual void Start()
    {
        //gameObject.AddComponent<ButtonDragScript>();
        //gameObject.AddComponent<BoxCollider2D>();
        Vector3 pos = new Vector3(transform.position.x + .7f, transform.position.y + 1, transform.position.z);
		options = (Canvas) Instantiate (PrefabManager.prefabManager.buildingOptionCanvas, pos, Quaternion.identity);
        options.transform.SetParent(this.transform);

        created = false;
        selected = true;
        placed = false;
        canDrag = false;
        showOptions = false;
        size = 1;
    }

    /**
     * On click functionality for building
     */
    void OnMouseDown()
    {
        /// bring up the building option panel
        /// which includes 1. move building. 2. upgrade building. 3. remove building. 4. info on leaf
        /// 
        /// 
        showOptions = !showOptions;
        options.gameObject.SetActive(showOptions);
		//
    }

    /**
     * Handles building drag behavior
     */
    void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector3 loc = BuildingManager.buildingManager.selectedTile.transform.position;
            this.transform.position = new Vector3(loc.x, loc.y, loc.z - 1);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
