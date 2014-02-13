using UnityEngine;
using System.Collections;

public abstract class ObstacleBall : Ball {

	/// <summary>
	/// The applied speed for the ball
	/// </summary>
	public int speed;

	delegate void DelayedMethod();
	
	void Awake()
	{
		GameManager.Instance.AddObstacleBall(gameObject);
	}

	// Use this for initialization
	public override void Start () {
		Activated = false;

		StartCoroutine(WaitAndDo(3, ActiveBall));

		base.Start();
	}

	public void ActiveBall()
	{


		Activated = true;
		ShootBall();
	}

	public void ShootBall()
	{
		// Randomize a random direction
		Vector3 V = new Vector3(Random.insideUnitCircle.x,Random.insideUnitCircle.y,0);
		// Normalize it (-1 to 1)
		V.Normalize();
		// Add the force to the random direction value

		rigidbody.AddForce(V * speed);
	}

	IEnumerator WaitAndDo(float time, DelayedMethod method)
	{
		yield return new WaitForSeconds(time);
		method();
	}
	void OnDestroy () {
		GameManager.Instance.DeleteObstacleBall(gameObject);
	}
}
