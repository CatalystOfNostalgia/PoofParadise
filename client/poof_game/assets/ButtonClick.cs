using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Why are we instantiating an object???
        //if (Input.GetMouseButtonDown(0))
      //  {
        //    Instantiate(target, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
        //}
	}
}
