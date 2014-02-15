using UnityEngine;
using System.Collections;

/// <summary>
/// A class for spawning gameobjects over the network.
/// </summary>
public class NetworkManager : MonoBehaviour {

	// The player ball to spawn
	public GameObject defaultPlayerBallPrefab;

	// The obstacle ball to spawn
	public GameObject defaultObstacleBallPrefab;

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start () 
	{

	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () 
	{
	
	}

	/// <summary>
	/// Spawns the main player ball at a random position.
	/// </summary>
	public void SpawnMainPlayerBall()
	{

		// Instantiate the main player ball
		GameObject playerBall = Instantiate(defaultPlayerBallPrefab, RandomPostition(), Quaternion.identity) as GameObject;;

		// Add the mouse input script to the ball
		playerBall.AddComponent("MouseInputInterface");

	}

	/// <summary>
	/// Spawns an obstacle ball.
	/// </summary>
	/// <returns>The obstacle ball.</returns>
	public GameObject SpawnObstacleBall()
	{
		// Instantiate the obstacle ball at a random position
		GameObject obstacleBall = Instantiate(defaultObstacleBallPrefab,  RandomPostition(), Quaternion.identity) as GameObject;

		// Set the tag
		obstacleBall.tag = "ObstacleBall";

		return obstacleBall;
	}

	/// <summary>
	/// Returns a random position on the board.
	/// </summary>
	/// <returns>The postition.</returns>
	Vector3 RandomPostition()
	{
		// Gets a random position on the x and y axis between a number range
		return new Vector3 (Random.Range (-620f, 620f), Random.Range (-230f, 340f), 0);
	}
}
