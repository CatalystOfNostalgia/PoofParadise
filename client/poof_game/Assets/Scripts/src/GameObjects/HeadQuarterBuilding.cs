using UnityEngine;
using System.Diagnostics;

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
    public override bool DeleteBuilding()
    {
        // Used stack trace to determine who called this method
        if (new StackTrace().GetFrame(1).GetMethod().Name == "UpgradeBuilding")
        {
            UnityEngine.Debug.Log("Upgrade called this");
            Destroy(this.gameObject);
            return true;
        }
        // If we didn't call upgrade, we shouldn't delete this
        else
        {
            Toast.toast.makeToast("You cannot destroy your headquarter building");
            return false;
        }
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

                UnityEngine.Debug.Log("found the tile");
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

    /**
     * Overrides the upgrade building feature of headquarters
     */
    public override bool UpgradeBuilding()
    {
        bool test = base.UpgradeBuilding();
        if (test)
        {
            SaveState.state.hqLevel++;
        }
        return test;
    }
}
