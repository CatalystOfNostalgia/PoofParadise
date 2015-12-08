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

    // Interface flags
    public bool canDrag { get; set; }
    public bool showOptions { get; set; }

    private Canvas options;

    // Use this for initialization
    protected virtual void Awake()
    {
        //gameObject.AddComponent<ButtonDragScript>();
        //gameObject.AddComponent<BoxCollider2D>();
        Vector3 pos = new Vector3(transform.position.x + .7f, transform.position.y + 1, transform.position.z);
		options = (Canvas) Instantiate (PrefabManager.prefabManager.buildingOptionCanvas, pos, Quaternion.identity);
        options.transform.SetParent(this.transform);

        /**
         * This section will surely fail once we implement saving and loading fully.
         * Everytime this building is 'built' we will deduct the cost from the users
         * available resources. As a result, loading the game may cost the user
         * a large amount of resources
         */
        DecorationBuildingInformation dbi;
        ResourceBuildingInformation rbi;
        if (SaveState.state.buildingInformationManager.DecorationBuildingInformationDict.TryGetValue(this.name.Replace("(Clone)", ""), out dbi))
        {
            // Set resource cost
            fireCost = dbi.FireCost;
            waterCost = dbi.WaterCost;
            earthCost = dbi.EarthCost;
            airCost = dbi.AirCost;

            // Spend allocated resources
            PayForBuilding();
            Debug.Log(string.Format("{0} was paid for", this.name));
        }
        else if (SaveState.state.buildingInformationManager.ResourceBuildingInformationDict.TryGetValue(this.name.Replace("(Clone)", ""), out rbi))
        {
            // Set resource cost
            fireCost = rbi.FireCost;
            waterCost = rbi.WaterCost;
            earthCost = rbi.EarthCost;
            airCost = rbi.AirCost;

            // Spend allocated resources
            PayForBuilding();
            Debug.Log(string.Format("{0} was paid for", this.name));
        }
        else
        {
            Debug.LogError(string.Format("Failed to spend resources on {0}", this.name));
            
        }

        created = false;
        selected = true;
        placed = false;
        canDrag = false;
        showOptions = false;
        size = 1;
    }

    /**
     * Pays for the building based on the cost of the building
     */
    private void PayForBuilding()
    {
        Debug.Log("Fire cost for this building is " + fireCost);
        if (ResourceIncrementer.incrementer.ResourceGain(-fireCost, ResourceBuilding.ResourceType.fire) &&
            ResourceIncrementer.incrementer.ResourceGain(-waterCost, ResourceBuilding.ResourceType.water) &&
            ResourceIncrementer.incrementer.ResourceGain(-earthCost, ResourceBuilding.ResourceType.earth) &&
            ResourceIncrementer.incrementer.ResourceGain(-airCost, ResourceBuilding.ResourceType.air))
        {
            Debug.Log("Successfully purchased building");
        }
        else
        {
            Tuple key = null;
            foreach (Tile t in TileScript.grid.tiles)
            {
                if (t.building != null)
                {
                    Debug.Log("Tile is not empty at ID:" + t.id);
                    if (t.building.Equals(this))
                    {
                        key = t.index;
                    }
                }
            } 
            // SaveState.state.buildings.Remove(key);
            Destroy(this.gameObject);
        }
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

    public override bool Equals(object o)
    {
        if (! this.GetType().Equals( o.GetType()))
        {
            return false;
        }

        return this.ID == (o as Building).ID;
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode() ^ this.GetType().GetHashCode();
    }
}
