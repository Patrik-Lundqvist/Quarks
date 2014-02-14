using UnityEngine;
using System.Collections;

public class Push : Spell {


	public Push()
	{
		powerCost = 10f;
	}

	public override void Precast()
	{
		FindTargets(3f);
		base.Precast();
	}

	public override void Casting()
	{

		EndCasting();
		foreach(GameObject ball in targets)
		{
			Vector3 dir = ball.transform.position - caster.transform.position;

			dir.Normalize();

			ball.gameObject.transform.rigidbody.velocity=dir * ball.rigidbody.velocity.magnitude * 0.8f;

		}

		base.Casting();

	}

}
