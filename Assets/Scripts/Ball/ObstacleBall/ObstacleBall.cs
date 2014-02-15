using UnityEngine;
using System.Collections;

public abstract class ObstacleBall : Ball {

	// The applied speed for the ball
	public int speed;

	// Delegate for a delayed method
	delegate void DelayedMethod();

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		GameManager.Instance.AddObstacleBall(gameObject);
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public override void Start () {
		isActivated = false;

		// Wait 3 seconds and then activate the ball
		StartCoroutine(WaitAndDo(3, ActiveBall));

		base.Start();
	}

	/// <summary>
	/// Actives the ball.
	/// </summary>
	public void ActiveBall()
	{
		isActivated = true;
		ShootBall();
	}

	/// <summary>
	/// Shoots the ball at a random direction.
	/// </summary>
	public void ShootBall()
	{
		// Randomize a random direction
		Vector3 V = new Vector3(Random.insideUnitCircle.x,Random.insideUnitCircle.y,0);
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
		GameManager.Instance.DeleteObstacleBall(gameObject);
	}
}
