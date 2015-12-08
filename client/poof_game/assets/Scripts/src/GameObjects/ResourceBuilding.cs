using UnityEngine;
using System;

[Serializable]
public class ResourceBuilding : Building {

	public enum ResourceType { fire, water, air, earth };

	// the resource collection rate and the type of resource
	public int collectionRate { get; set;}
	public ResourceType type;

}
