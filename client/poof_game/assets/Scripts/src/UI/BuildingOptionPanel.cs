﻿using UnityEngine;
using UnityEngine.UI;

/**
 * The GamePanel which is attached to each 
 * building
 * Features include delete, drag, upgrade, and info
 */
public class BuildingOptionPanel : GamePanel {

	// Move, upgrade, remove, info
	private Button[] buttons;
    private Building building;

	// TODO Make the menu set inactive when you press outside

	/**
     * Overrides the start functionality 
     * provided by GamePanel
     */

	override public void Start () {
		buttons = RetrieveButtonList ("Buttons");
        SetBuilding();
		GeneratePanel ();
	}

    /**
     * Overrides the GeneratePanel functionality
     * provided bu GamePanel
     */
	override public void GeneratePanel(){
        FindAndModifyUIElement("Move Button", buttons, () => { building.MoveBuilding();});
		FindAndModifyUIElement("Upgrade Button", buttons, ()=> UpgradePanel.upgradePanel.TogglePanel());
		FindAndModifyUIElement("Remove Button", buttons, ()=> building.DeleteBuilding());
		FindAndModifyUIElement("Info Button", buttons, ()=> Debug.Log("Info button is pressed"));
	}

	/**
     * A helper method that finds the building
     */
	public Building getNewBuilding(ResourceBuilding[] list, string find){
		for (int i=0; i<=list.Length; i++) {
			if(list[i].name == find){
				return list[i];
			}
		}
		return null;
	}

    /**
     * Sets the building reference for this panel
     */
    private void SetBuilding()
    {
        building = this.transform.GetComponentInParent<Building>();
    }
}
