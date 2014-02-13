using UnityEngine;
using System.Collections;

public class MouseInputInterface : MonoBehaviour {
	
	float ballDistanceFromCamera;

	// calculation variables
	float current_x = 0.0f;
	float current_y = 0.0f;
	

	// string references to speed string manipulation/calculation
	string x_axis = "Horizontal";
	string y_axis = "Vertical";

	float screen_width;

	
	// Use this for initialization
	public void Start () {
		Screen.showCursor = false;
		ballDistanceFromCamera = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);

		// Calculate the screen width
		screen_width = Camera.main.orthographicSize * Camera.main.aspect;
	}
		
	// Update is called once per frame
	public void Update () {
			
		if (DidMove()) {
			Vector3 v3Pos = Input.mousePosition;

			// Get distance the paddle is in front of the camera
			v3Pos.z = ballDistanceFromCamera;
			v3Pos = Camera.main.ScreenToWorldPoint (v3Pos);
			v3Pos.x = Mathf.Clamp(v3Pos.x, -screen_width +  (renderer.bounds.size.x / 2) , screen_width -  (renderer.bounds.size.x / 2));
			v3Pos.y = Mathf.Clamp(v3Pos.y, -7 +  (renderer.bounds.size.y / 2) , 10 -  (renderer.bounds.size.y / 2));
			transform.position = v3Pos;
		}

	}

	//initialize your variables here
	void Awake() {
		// initialize your current x with the current change in x axis
		current_x = Input.GetAxis(x_axis);
		current_y = Input.GetAxis(y_axis);
	}

	bool DidMove() {
		// get the current change in horizontal mouse movement
		current_x = Input.GetAxis(x_axis);
		current_y = Input.GetAxis(y_axis);

	

		// check if the current x has a value of 0. if not, return true;
		if (current_x == 0 && current_y == 0)
			return false;
		else
			return true;
	}
}
