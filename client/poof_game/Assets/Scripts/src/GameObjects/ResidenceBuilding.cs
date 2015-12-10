/**
 * The Residence Building extends Building
 * It serves to provided more homes for poofs
 */
public class ResidenceBuilding : Building {

    // Fields
    public int poofsAllowed;

    /**
     * Each new poof residence bumps the limit by 2
     */
    protected override void Awake()
    {
        base.Awake();
        SaveState.state.poofLimit += 2;
        PoofCounterPanel.poofCounterPanel.GeneratePanel();
    }
}
