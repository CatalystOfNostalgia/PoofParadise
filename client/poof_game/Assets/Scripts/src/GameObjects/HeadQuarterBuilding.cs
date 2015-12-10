using UnityEngine;

/**
 * The Headquarters Building extends Buidling
 * It serves as the headquarters for the game
 */
public class HeadQuarterBuilding : Building {

	// TODO stuff hq building might need
	// for example, poof generation limit, building requirement limit

	public int poofAllowed;
	public Building[] canBuildList {get; set;}

    /**
     * Overrides the delete building call for headquarters
     */
    public override void DeleteBuilding()
    {
        Destroy(this.gameObject);
    }

    /**
     * Overrides the movement call for headquarters
     */
    public override void MoveBuilding()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        canDrag = true;

        foreach (Tile t in TileScript.grid.tiles)
        {
            if (t.building != null && t.building.Equals(this))
            {
                // Store this key and remove any memory of the building from the tiles
                t.isVacant = true;
                if (t.leftTile != null) { t.leftTile.isVacant = true; }
                if (t.downTile != null) { t.downTile.isVacant = true; }
                if (t.downLeftTile != null) { t.downLeftTile.isVacant = true; }
                t.building = null;
            }
        }

        showOptions = !showOptions;
        options.gameObject.SetActive(showOptions);
    }
}
