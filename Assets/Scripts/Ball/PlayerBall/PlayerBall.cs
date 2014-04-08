using UnityEngine;
using System.Collections;

/// <summary>
/// Player ball.
/// </summary>
public abstract class PlayerBall : Ball {
	

	// The regeneration rate of the power
	private float powerRegRate = 2f;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		PlayerManager.Instance.SetPlayerBall(this.gameObject);
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public override void Start () {
		isActivated = true;
		base.Start();
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public override void Update () {
	
		base.Update();
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public override void OnCollisionEnter(Collision collision) {

		base.OnCollisionEnter(collision);

		// Get the object name of the object which we collided with
		var hitobject = collision.gameObject.tag;

		if(hitobject == "ObstacleBall")
		{
			// If we are not invulnerable, game over
			if(!isInvulnerable)
			{
				GameOver();
			}

		}
	
	}

	/// <summary>
	/// If we trigger any of the trigger colliders
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)	
	{	
		// Get the object name of the object which we collided with
		var hitobject = other.gameObject.tag;

		if(hitobject == "PowerReg")
		{

			// Draw a sprite at the others balls position
			other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		
	}

	/// <summary>
	/// When we leave a trigger
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit(Collider other)
	{
		// Get the object name of the object which we collided with
		var hitobject = other.gameObject.tag;

		if(hitobject == "PowerReg")
		{
			// Delete the sprite
			other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	/// <summary>
	/// This is called once per frame for every collision
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerStay(Collider other)
	{
		// Get the object name of the object which we collided with
		var hitobject = other.gameObject.tag;
		
		if(hitobject == "PowerReg")
		{
			// Make sure we're not at full power
			if(PlayerManager.Instance.powerCurrent < 100)
				PlayerManager.Instance.powerCurrent += powerRegRate * Time.deltaTime;
		}
		
	}

	/// <summary>
	/// Initiate the game over sequence
	/// </summary>
	void GameOver ()
	{
		// Find all power reg objects
		var obs = GameObject.FindGameObjectsWithTag("PowerReg");

		// Disable all the power reg sprites
		foreach (var ob in obs) {
			ob.GetComponent<SpriteRenderer>().enabled = false;
		}

		GameManager.Instance.GameOver();

		Destroy(this.gameObject);


	}

	/// <summary>
	/// Destroy event.
	/// </summary>
	void OnDestroy () {
		// Delete the ball from the main list
		PlayerManager.Instance.DeletePlayerBall();
	}

}
