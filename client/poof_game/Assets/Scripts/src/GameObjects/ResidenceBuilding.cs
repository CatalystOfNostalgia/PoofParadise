using UnityEngine;
using System.Collections;

public class ResidenceBuilding : Building {

    // Fields
    public int poofAllowed;

    // Each new poof residence bumps the limit by 2
    protected override void Start()
    {
        SaveState.state.poofLimit += 2;
    }
}
