using UnityEngine;
using System.Collections;

public class DecorativeBuilding : Building {

	// Fields
	public int poofGenerationRate;

	protected virtual void Start()
	{
        base.Awake();
        
        PoofManager.poofManager.beamDownPoof(poofGenerationRate);
        Debug.Log("[DecorativeBuilding] Beam down poof");
	}
}
