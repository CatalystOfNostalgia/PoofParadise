using UnityEngine;
using System.Collections;

public class BuildingInfo : MonoBehaviour {
	private string name;
	private string description;
	private int fireCost;
	private int waterCost;
	private int airCost;
	private int earthCost;

	public BuildingInfo(string name, string description, int fireCost, int waterCost, int airCost, int earthCost){
		this.name = name;
		this.description = description;
		this.fireCost = fireCost;
		this.waterCost = waterCost;
		this.airCost = airCost;
		this.earthCost = earthCost;
	}

	public string Name { get; }
	public string Description { get; }
	public int FireCost { get; }
	public int WaterCost { get; }
	public int AirCost { get; }
	public int EarthCost { get; }
}
