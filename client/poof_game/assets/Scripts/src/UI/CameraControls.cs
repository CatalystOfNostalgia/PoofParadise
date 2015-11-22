using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
	
	public float speed = 0.5F;
	public float sensitivity = 30F;
	public float xEdge = 40;
	public float yEdge = 40;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var pos = this.transform.position;
		if (Input.mousePosition.x < sensitivity && pos.x > (-xEdge+4.95)) {
			//move camera left
			pos.x -= speed;
		}
		else if (Input.mousePosition.x > Screen.width - sensitivity && pos.x < (xEdge+4.95)) {
			//move camera right
			pos.x += speed;
		}
		if (Input.mousePosition.y < sensitivity && pos.y > -yEdge) {
			// move camera down
			pos.y -= speed;
		}
		else if (Input.mousePosition.y > Screen.height - sensitivity && pos.y < yEdge) {
			// move camera up
			pos.y += speed;
		}

		var d = Input.GetAxis("Mouse ScrollWheel");
		if (d > 0f && Camera.main.orthographicSize > 2f)
		{
			Camera.main.orthographicSize -= speed;
		}
		else if (d < 0f && Camera.main.orthographicSize < 6f)
		{
			Camera.main.orthographicSize += speed;
		}

		this.transform.position = pos;


	}
}