/**
 * The Headquarters Building extends Buidling
 * It serves as the headquarters for the game
 */
public class HeadQuarterBuilding : Building {

	// TODO stuff hq building might need
	// for example, poof generation limit, building requirement limit

	public int poofAllowed;
	public Building[] canBuildList {get; set;}

}
