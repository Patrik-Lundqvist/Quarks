using UnityEngine;
using System.Collections;

public abstract class PlayerBall : Ball {

	public int powerReg;
	private float powerRegRate = 2f;


	void Awake()
	{
		PlayerManager.Instance.SetPlayerBall(this.gameObject);
	}

	// Use this for initialization
	public override void Start () {
		Activated = true;
		base.Start();
	}
	// Use this for initialization
	public override void Update () {

		if(powerReg > 0)
		{
			if(PlayerManager.Instance.powerCurrent < 100)
				PlayerManager.Instance.powerCurrent += powerRegRate * Time.deltaTime;
		}


		base.Update();
	}
	
	public override void OnCollisionEnter(Collision collision) {


		base.OnCollisionEnter(collision);

		string hitobject = collision.gameObject.tag;

		if(hitobject == "ObstacleBall")
		{
			GameOver();
		}
	
	}

	void OnTriggerEnter(Collider other)	
	{	
		string hitobject = other.gameObject.tag;


		if(hitobject == "ManaReg")
		{
			powerReg += 1;
			other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
		
	}

	void OnTriggerExit(Collider other)
	{
		string hitobject = other.gameObject.tag;

		if(hitobject == "ManaReg")
		{
			powerReg -= 1;
			other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void GameOver ()
	{
		GameObject[] obs = GameObject.FindGameObjectsWithTag("ManaReg");
		
		foreach (GameObject ob in obs) {
			ob.GetComponent<SpriteRenderer>().enabled = false;
		}

		Destroy(this.gameObject);

		GameManager.Instance.GameOver();
	}
}
