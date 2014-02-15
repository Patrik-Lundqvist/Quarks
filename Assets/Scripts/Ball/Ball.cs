﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for ball behaviour
/// </summary>
public abstract class Ball : MonoBehaviour {

	// Variables for storing ball state
	private Vector3 savedVelocity;
	private Vector3 savedAngularVelocity;

	// The different ball materials
	public Material ballMaterial;
	public Material ballMaterialDeActived;

	// Physical material of the ball
	public PhysicMaterial ballPhysMaterial;

	// Is the ball active
	private bool _isActivated;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Ball"/> is activated.
	/// </summary>
	/// <value><c>true</c> if is activated; otherwise, <c>false</c>.</value>
	public bool isActivated
	{
		get { return _isActivated; }
		set 
		{ 
			if(value)
			{
				// Make sure the ball is activated
				ActivateBall();
			}
			else
			{
				// Make sure the ball is disabled
				DisableBall();
			}

			// Set the value
			_isActivated = value; 
		}
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public virtual void Start () {
		if(isActivated){
			renderer.material = ballMaterial;
		}
		collider.material = ballPhysMaterial;
	}

	/// <summary>
	/// Disables the ball.
	/// </summary>
	public void DisableBall(){

		// Stop the ball
		StopBall();

		// Change the material
		renderer.material = ballMaterialDeActived;

		// Disable all collisions
		rigidbody.detectCollisions = false;

		// Move the ball back a bit
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,8f);
	}

	/// <summary>
	/// Activates the ball.
	/// </summary>
	public void ActivateBall(){

		// Make the ball move again
		StartBall();

		// Change the material
		renderer.material = ballMaterial;

		// Activate all collision detectors
		rigidbody.detectCollisions = true;

		// Move the ball back to the correct level
		rigidbody.position = new Vector3(rigidbody.position.x,rigidbody.position.y,0f);
	}

	/// <summary>
	/// Stops the ball.
	/// </summary>
	public void StopBall()
	{
		// Save the velocity
		savedVelocity = rigidbody.velocity;
		// Save the direction
		savedAngularVelocity = rigidbody.angularVelocity;
		// Make sure the ball can't move
		rigidbody.isKinematic = true;
	}

	/// <summary>
	/// Starts the ball.
	/// </summary>
	public void StartBall()
	{
		// Make sure the ball can move
		rigidbody.isKinematic = false;
		// Add the saved velocity
		rigidbody.AddForce( savedVelocity, ForceMode.VelocityChange );
		// Add the saved direction
		rigidbody.AddTorque( savedAngularVelocity, ForceMode.VelocityChange );
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void OnCollisionEnter (Collision collision) {


		// If the ball is an obstacle ball, play audio clip
		if(collision.gameObject.tag == "ObstacleBall")
		{
			// The offset
			float audioClipOffset = 445.0f;

			// Get the velocity and apply offset
			float p = collision.relativeVelocity.magnitude / audioClipOffset;

			// Play audio clip
			new OTSound("BallHit").Pitch(Mathf.Clamp( p, 0.5f, 1.1f)).Volume(Mathf.Clamp( p, 0.2f, 1.5f));
		}

	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public virtual void Update () {
		
	}
}
