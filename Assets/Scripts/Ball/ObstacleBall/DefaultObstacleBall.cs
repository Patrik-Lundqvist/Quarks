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

		base.Start();
	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	public override void Update () {
		base.Update();
	}


	public override void ActivateBall () {

		// Enable power reg
		transform.FindChild("PowerReg").collider.enabled = true;
		
		base.ActivateBall();
	}

	
	public override void DisableBall () {

		// Disable power reg
		transform.FindChild("PowerReg").collider.enabled = false;

		base.DisableBall();
	}
}
