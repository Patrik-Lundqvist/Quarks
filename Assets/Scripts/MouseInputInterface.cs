using UnityEngine;
using System.Collections;

public class MouseInputInterface : MonoBehaviour {

	// The mouse sense
	float mouseSpeed = 1f;

	// Calculation variables
	float current_x = 0.0f;
	float current_y = 0.0f;
	
	Transform cachedTransform;

	// String references to speed string calculation
	string x_axis = "Horizontal";
	string y_axis = "Vertical";

	float screen_width;

	
	// Use this for initialization
	public void Start () {

		// Calculate the screen width
		screen_width = Camera.main.orthographicSize * Camera.main.aspect;

	}
		
	// Update is called once per frame
	public void Update () {
		MouseMove();
	}

	void Awake() {
		// Initialize the current x with the current change in x axis
		current_x = Input.GetAxis(x_axis);
		current_y = Input.GetAxis(y_axis);
	}

	void MouseMove()
	{
		// Get the current change in horizontal and vertical mouse movement
		current_x = Input.GetAxis(x_axis);
		current_y = Input.GetAxis(y_axis);

		// Check if the current x and y has changed
		if (current_x != 0 || current_y != 0)
		{
			// Cache the current position
			cachedTransform = transform;
			// Move the cached position with mouse movement
			cachedTransform.Translate(-current_x * Time.deltaTime * 16.5f * mouseSpeed, current_y * Time.deltaTime * 16.5f  * mouseSpeed, 0);

			// Get the xyz position data from the cached position
			Vector3 v3Pos = cachedTransform.position;

			// Get the new postition to the ball with a constraint of the screen width and height
			v3Pos.x = Mathf.Clamp(v3Pos.x, -screen_width +  (renderer.bounds.size.x / 2) , screen_width -  (renderer.bounds.size.x / 2));
			v3Pos.y = Mathf.Clamp(v3Pos.y, -7 +  (renderer.bounds.size.y / 2) , 10 -  (renderer.bounds.size.y / 2));

			// Apply the position to the ball
			transform.position = v3Pos;
		}


	}
}
