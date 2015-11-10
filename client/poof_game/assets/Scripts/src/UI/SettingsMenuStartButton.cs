using UnityEngine;
using System.Collections;
/**
 * This class is basically TestModalPanel, but for Settings menu
 */
public class SettingsMenuStartButton : MonoBehaviour {

	public void activateMenu(){
		SettingsMenu.menu.gameObject.SetActive (true);
	}
}
