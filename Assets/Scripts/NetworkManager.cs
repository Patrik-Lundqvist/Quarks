using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject defaultPlayerBallPrefab;
	public GameObject defaultObstacleBallPrefab;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void SpawnMainPlayerBall()
	{

		Vector3 v3Pos = Input.mousePosition;

		// Calculate the screen width
		float screen_width = Camera.main.orthographicSize * Camera.main.aspect;

		// Get distance the paddle is in front of the camera
		v3Pos.z = Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
		v3Pos = Camera.main.ScreenToWorldPoint (v3Pos);
		v3Pos.x = Mathf.Clamp(v3Pos.x, -screen_width +  (defaultPlayerBallPrefab.renderer.bounds.size.x / 2) , screen_width -  (defaultPlayerBallPrefab.renderer.bounds.size.x / 2));
		v3Pos.y = Mathf.Clamp(v3Pos.y, -7 +  (defaultPlayerBallPrefab.renderer.bounds.size.y / 2) , 10 -  (defaultPlayerBallPrefab.renderer.bounds.size.y / 2));
		v3Pos.z = 0;
		GameObject playerBall = Instantiate(defaultPlayerBallPrefab, v3Pos, Quaternion.identity) as GameObject;;


		playerBall.AddComponent("MouseInputInterface");

	}

	public GameObject SpawnObstacleBall()
	{
		GameObject obstacleBall = Instantiate(defaultObstacleBallPrefab,  RandomPostition(), Quaternion.identity) as GameObject;
		obstacleBall.tag = "ObstacleBall";
		return obstacleBall;
		
	}

	Vector3 RandomPostition()
	{
		return new Vector3 (Random.Range (-16.0f, 16.0f), Random.Range (-6f, 9f), 0);
	}
}
