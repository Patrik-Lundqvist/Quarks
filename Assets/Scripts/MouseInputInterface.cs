using UnityEngine;
using System.Collections;

/// <summary>
/// Mouse input class for controlling the player ball
/// </summary>
public class MouseInputInterface : MonoBehaviour {

	// The mouse sense
	float mouseSpeed;

	// Calculation variables for the axis
	float current_x = 0.0f;
	float current_y = 0.0f;

	// String references to the axis names
	string x_axis = "Horizontal";
	string y_axis = "Vertical";

	// Variable for the screen width
	float screen_width;

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public void Start () 
	{

		// Calculate the screen width
		screen_width = Camera.main.orthographicSize * Camera.main.aspect;
		mouseSpeed = PlayerPrefs.GetFloat("mouseSense");
	}
		
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public void Update () 
	{
		// Check if we need to update position
		MouseMove();
	}

	/// <summary>
	/// Checks if the mouse has moved and moves the ball.
	/// </summary>
	void MouseMove()
	{
		// Get the current change in horizontal and vertical mouse movement
		current_x = Input.GetAxis(x_axis);
		current_y = Input.GetAxis(y_axis);

		// Check if the current x and y has changed
		if (current_x != 0 || current_y != 0)
		{
			// Cache the current position in a variable
			Transform cachedTransform = transform;

			// Move the cached position with mouse movement
			cachedTransform.Translate(current_x * Time.deltaTime * 600f * mouseSpeed, current_y * Time.deltaTime * 600f  * mouseSpeed, 0);

			// Get the xyz position data from the cached position
			Vector3 v3Pos = cachedTransform.position;

			// Get the new postition to the ball with a constraint of the screen width and height
			v3Pos.x = Mathf.Clamp(v3Pos.x, -screen_width +  (renderer.bounds.size.x / 2) , screen_width -  (renderer.bounds.size.x / 2));
			v3Pos.y = Mathf.Clamp(v3Pos.y, -234 , 342.5f);

			// Apply the position to the ball
			transform.position = v3Pos;
		}

	}
}
