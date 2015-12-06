using UnityEngine;
using System.Collections;

public class HeadQuarterBuilding : Building {

	// TODO stuff hq building might need
	// for example, poof generation limit, building requirement limit

	public int poofAllowed;
	public Building[] canBuildList {get; set;}

}
