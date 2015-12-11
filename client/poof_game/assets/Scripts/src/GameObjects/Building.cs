using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/**
 * Building serves as an abstract MonoBehavior which 
 * can be extended for each type of building
 * Basic building functionality is implemented here
 */
[RequireComponent(typeof(Physics2DRaycaster))]
public abstract class Building : MonoBehaviour {

    private bool selected { get; set; }
    private bool placed { get; set; }

    // database management data
    public bool created;
    public int buildingInfoID;
    public int level;

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

    public Canvas options { get; set; }
	private Animator animator;

    // Use this for initialization
    protected virtual void Awake()
    {
        Vector3 pos = new Vector3(transform.position.x + .7f, transform.position.y + 1, transform.position.z);
		options = (Canvas) Instantiate (PrefabManager.prefabManager.buildingOptionCanvas, pos, Quaternion.identity);
        options.transform.SetParent(this.transform);

        level = 1;
        created = true;
        selected = true;
        placed = false;
        canDrag = false;
        showOptions = false;
        size = 1;

		animator = this.GetComponent<Animator>();
    }

	public void constructionAnimation(){
		animator.SetInteger("Construction", 1);
		Invoke("constructionFinish", 5f);
	}

	public void constructionFinish(){
		animator.SetInteger("Construction", 0);
	}

    /**
     * Pays for the building based on the cost of the building
     */
    public bool PayForBuilding()
    {

        Debug.Log("paying for a building");
        // Pulls the cost of this building from Building Information Manager
        DecorationBuildingInformation dbi;
        ResourceBuildingInformation rbi;
        if (SaveState.state.buildingInformationManager.DecorationBuildingInformationDict.TryGetValue(this.name.Replace("(Clone)", ""), out dbi))
        {
            // Set resource cost
            fireCost = dbi.FireCost;
            waterCost = dbi.WaterCost;
            earthCost = dbi.EarthCost;
            airCost = dbi.AirCost;
        }
        else if (SaveState.state.buildingInformationManager.ResourceBuildingInformationDict.TryGetValue(this.name.Replace("(Clone)", ""), out rbi))
        {
            // Set resource cost
            fireCost = rbi.FireCost;
            waterCost = rbi.WaterCost;
            earthCost = rbi.EarthCost;
            airCost = rbi.AirCost;
        }
        else
        {
            return true;
        }

        // Attempts to purchases the building -> Errors may result for buildings with multiple costs
        if (ResourceIncrementer.incrementer.ResourceGain(-fireCost, ResourceBuilding.ResourceType.fire) &&
            ResourceIncrementer.incrementer.ResourceGain(-waterCost, ResourceBuilding.ResourceType.water) &&
            ResourceIncrementer.incrementer.ResourceGain(-earthCost, ResourceBuilding.ResourceType.earth) &&
            ResourceIncrementer.incrementer.ResourceGain(-airCost, ResourceBuilding.ResourceType.air)) {

            return true;
        }

        // Otherwise, fail to purchase building
        else
        {
            return false;
        }
    }

    /**
     * On click functionality for building
     * Pulls open the building option panel
     */
    void OnMouseDown()
    {
        if (!canDrag)
        {
            showOptions = !showOptions;
            options.gameObject.SetActive(showOptions);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /**
     * Handles building drag behavior
     * Allows buildings to be dragged
     * TODO: Fix this functionality
     */
    void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector3 loc = BuildingManager.buildingManager.selectedTile.transform.position;
			this.transform.position = new Vector3(loc.x, loc.y - .25f, loc.y - .25f);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /**
     * Handles the end drag state
     * this could probably be done cleaner by calling place building instead of
     * manually setting the tiles
     */
    void OnMouseUp()
    {
        if (canDrag)
        {
            canDrag = false;
            SaveState.state.addBuilding(BuildingManager.buildingManager.selectedTile.index, this);
            BuildingManager.buildingManager.selectedTile.isVacant = false;
            BuildingManager.buildingManager.selectedTile.leftTile.isVacant = false;
            BuildingManager.buildingManager.selectedTile.downTile.isVacant = false;
            BuildingManager.buildingManager.selectedTile.downLeftTile.isVacant = false;
            BuildingManager.buildingManager.selectedTile.building = this;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    /**
     * Serves as the function for deleting this building from the game
     */
    public virtual void DeleteBuilding()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        bool remove = false;
        Tuple key = GetTupleFromGrid();
        remove = SaveState.state.removeBuilding(key);

        if (remove)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogError(string.Format("[Building] Unable to locate {0} at {1} in the building dictionary", this.name, key));
        }
    }

    /**
     * Provides the movement functionality for this building
     */
    public virtual void MoveBuilding()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        canDrag = true;
        Tuple key = GetTupleFromGrid();
        bool remove = SaveState.state.removeBuilding(key);

        if (!remove)
        {
            Debug.LogError(string.Format("[Building] Unable to locate {0} at {1} in the building dictionary", this.name, key));
        }
        showOptions = !showOptions;
        options.gameObject.SetActive(showOptions);
    }

    /**
     * Upgrades building to level 2 resource building 
     */
    public virtual void UpgradeBuilding(){
		UpgradePanel.upgradePanel.gameObject.SetActive (false);
        Building upgrade = null;
        string newName = this.name.Replace("Lvl 1(Clone)", "Lvl 2");
        foreach (Building b in PrefabManager.prefabManager.buildings)
        {
            if (b.name == newName)
            {
                upgrade = b;
            }
        }

        // If upgrade does not exist
        if (upgrade == null)
        {
            Debug.LogError(string.Format("{0} does not exist", newName));
        }
        else
        {
            Instantiate(upgrade);
            SaveState.state.earth = SaveState.state.earth - this.earthCost;
            SaveState.state.water = SaveState.state.water - this.waterCost;
            SaveState.state.air = SaveState.state.air - this.airCost;
            SaveState.state.fire = SaveState.state.fire - this.fireCost;
            this.DeleteBuilding();
        }
    }

    /**
     * Provides comparison functionality for buildings
     */
    public override bool Equals(object o)
    {
        if (! this.GetType().Equals( o.GetType()))
        {
            return false;
        }

        return this.name == (o as Building).name;
    }

    /**
     * Provides hashcode functionality for buildings
     */
    public override int GetHashCode()
    {
        return ID.GetHashCode() ^ this.GetType().GetHashCode();
    }

    /**
     * Retrieves the tuple that this building is on
     * then sets the tiles it was sitting on free
     */
    private Tuple GetTupleFromGrid()
    {
        Tuple key = null;
        foreach (Tile t in TileScript.grid.tiles)
        {
            if (t.building != null && t.building.Equals(this))
            {
                // Store this key and remove any memory of the building from the tiles
                key = t.index;
                t.isVacant = true;
                if ( t.leftTile != null ) { t.leftTile.isVacant = true; }
                if ( t.downTile != null ) { t.downTile.isVacant = true; }
                if ( t.downLeftTile != null ) { t.downLeftTile.isVacant = true; }
                t.building = null;
            }
        }
        return key;
    }
}
