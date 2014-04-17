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


    public override void StartBall()
    {

		// Enable power reg
		transform.FindChild("PowerReg").collider.enabled = true;

        base.StartBall();
	}


    public override void StopBall()
    {

        var powerReg = transform.FindChild("PowerReg");

		// Disable power reg
        powerReg.collider.enabled = false;
        powerReg.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        base.StopBall();
	}
}
