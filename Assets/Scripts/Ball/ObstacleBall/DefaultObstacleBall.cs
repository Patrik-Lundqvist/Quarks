using UnityEngine;
using System.Collections;

/// <summary>
/// Default obstacle ball.
/// </summary>
public class DefaultObstacleBall : ObstacleBall {
	
	/// <summary>
	/// Use this for initialization.
	/// </summary>
	public override void Start () {

		// Sets the ball speed
		speed = 15000;

		base.Start();
	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public override void Update () {
		base.Update();
	}
}
