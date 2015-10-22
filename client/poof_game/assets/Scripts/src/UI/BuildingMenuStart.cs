using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingMenuStart : MonoBehaviour {

	public BuildingMenu buildMenu;

	public void activateBuildMenu(){
		buildMenu.Choice();
	}
}
