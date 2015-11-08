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

	private void sampleAddInfo(){

	}
}
