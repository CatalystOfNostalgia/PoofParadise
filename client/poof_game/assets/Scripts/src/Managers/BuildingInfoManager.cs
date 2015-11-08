using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingInfoManager : MonoBehaviour {

	private Dictionary<string, BuildingInfo> infoDict;

	// Use this for initialization
	void Start () {
		infoDict = new Dictionary<string, BuildingInfo> ();
	}

	public BuildingInfo getInfo(string key){
		BuildingInfo value;
		if (infoDict.TryGetValue(key, out value)){
			return value;
		}
		return null;
	}

	public void populateDictFromJSON(){

	}

	public void sampleAddInfo(){
		infoDict.Add ("Fire Tree", new BuildingInfo ("Fire Tree", "It's a burning Tree!", 200, 0, 0, 0));
		infoDict.Add ("Pond", new BuildingInfo ("Pond", "Poofs pee in this pond", 0, 200, 0, 0));
		infoDict.Add ("Windmill", new BuildingInfo ("Windmill", "Spinning~", 0, 0, 200, 0));
		infoDict.Add ("Fire Tree", new BuildingInfo ("Cave", "It's dark in here", 0, 0, 0, 200));
	}
}
