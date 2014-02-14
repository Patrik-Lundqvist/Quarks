using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell : MonoBehaviour {

	public SpellStatus status;
	
	public GameObject caster;

	public List<GameObject> targets;

	public float powerCost;

	public virtual void Setup(GameObject caster)
	{
		targets = new List<GameObject>();
		this.caster = caster;
	}

	public virtual void Precast()
	{
		if(PlayerManager.Instance.powerCurrent < powerCost)
		{
			NoPower();
			return;
		}

		PlayerManager.Instance.powerCurrent -= powerCost;
		status = SpellStatus.Casting;
	}
	
	public virtual void  Casting()
	{
		EndCasting();
	}

	public virtual void EndCasting()
	{
		status = SpellStatus.Finished;
		CleanUp();
	}

	public virtual void  CleanUp()
	{
		status = SpellStatus.Ready;

		Destroy(this);
	}

	public virtual void Cast()
	{
		Precast();
	}

	public void FindTargets(float Radius)
	{
		targets.Clear();

		Collider[] Colliders;

		Colliders = Physics.OverlapSphere(caster.gameObject.transform.position, Radius);

		foreach (Collider hit in Colliders) {
			if (hit && hit.tag == "ObstacleBall"){

				targets.Add(hit.gameObject);
			}
		}


	}

	void NoPower()
	{
		CleanUp();
	}

	void Update()
	{
		if(status == SpellStatus.Casting)
		{
			Casting();
		}
	}
}
