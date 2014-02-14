using UnityEngine;
using System.Collections;

public class DefaultObstacleBall : ObstacleBall {
	
	// Use this for initialization
	public override void Start () {
		// Sets the ball speed
		speed = 400;

		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
