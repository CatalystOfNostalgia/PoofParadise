using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ResourceBuilding : Building {

	public enum ResourceType { fire, water, air, earth };

	// the resource collection rate and the type of resource
	public int collectionRate { get; set;}
	public ResourceType type;

	// Use this for initialization
	protected override void Start () {
        base.Start();
		collectionRate = 5;
		switch (type){
		case ResourceType.air:
			InvokeRepeating ("tickAir", .01f, 1.0f);
			break;
		case ResourceType.earth:
			InvokeRepeating ("tickEarth", .01f, 1.0f);
			break;
		case ResourceType.fire:
			InvokeRepeating ("tickFire", .01f, 1.0f);
			break;
		case ResourceType.water:
			InvokeRepeating ("tickWater", .01f, 1.0f);
			break;
		default:
			Debug.Log("ResourceBuilding: Illegal resource type");
			break;
		}
	}

	//copypasta code because InvokeRepeating can't take in param.
	void tickFire (){
		ResourceIncrementer.incrementer.ResourceGain (collectionRate, ResourceType.fire);
	}
	void tickWater (){
		ResourceIncrementer.incrementer.ResourceGain (collectionRate, ResourceType.water);
	}
	void tickAir (){
		ResourceIncrementer.incrementer.ResourceGain (collectionRate, ResourceType.air);
	}
	void tickEarth (){
		ResourceIncrementer.incrementer.ResourceGain (collectionRate, ResourceType.earth);
	}

}
