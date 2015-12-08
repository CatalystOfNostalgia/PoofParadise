using UnityEngine;

/**
 * The Decorative Building MonoBehavior extends Building
 * It is a specific type of building which attracts poofs
 * to the user's paradise
 */
public class DecorativeBuilding : Building {

	// Fields
	public int poofGenerationRate;

	protected override void Awake()
	{
        base.Awake();
        
        PoofManager.poofManager.beamDownPoof(poofGenerationRate);
        Debug.Log("[DecorativeBuilding] Beam down poof");
	}
}
