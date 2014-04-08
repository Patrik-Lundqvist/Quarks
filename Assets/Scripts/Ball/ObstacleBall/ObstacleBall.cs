using UnityEngine;
using System.Collections;

public abstract class ObstacleBall : Ball {

	// The applied speed for the ball
	public int speed;

	// Delegate for a delayed method
	delegate void DelayedMethod();

	// If the ball is in a live game
	public bool isLive;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		isActivated = false;
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public override void Start () {

		if(isLive)
		{
			// Wait 3 seconds and then activate the ball
			StartCoroutine(WaitAndDo(3, ShootBall));
		}


		base.Start();
	}
	

	/// <summary>
	/// Activates and shoots the ball at a random direction.
	/// </summary>
	public void ShootBall()
	{
		isActivated = true;

		// Randomize a random direction
		var V = new Vector3(Random.insideUnitCircle.x,Random.insideUnitCircle.y,0);
		// Normalize it (-1 to 1)
		V.Normalize();

		// Add the force to the random direction value
		rigidbody.AddForce(V * speed);
	}

	/// <summary>
	/// Waits a number of seconds and then fires the passed method.
	/// </summary>
	/// <returns>The and do.</returns>
	/// <param name="time">Time.</param>
	/// <param name="method">Method.</param>
	IEnumerator WaitAndDo(float time, DelayedMethod method)
	{
		yield return new WaitForSeconds(time);
		method();
	}
	/// <summary>
	/// Destroy event.
	/// </summary>
	void OnDestroy () {
		if(isLive)
			GameManager.Instance.DeleteObstacleBall(gameObject);
	}
}
