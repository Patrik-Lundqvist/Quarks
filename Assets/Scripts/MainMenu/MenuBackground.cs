using UnityEngine;
using System.Collections;

public class MenuBackground : MonoBehaviour {

    // The obstacle ball to spawn
    public GameObject DefaultObstacleBallPrefab;

	// Use this for initialization
	void Start () {

        // Set the global gravity to 0
        Physics.gravity = new Vector3(0, 0, 0);

        // Spawn background balls
        SpawnObstacleBalls(5);
	}

    private void SpawnObstacleBalls(int numberOfBalls)
    {
        for (var i = 0; i < numberOfBalls; i++)
        {
            // Instantiate the obstacle ball at a random position
            var obstacleBall = Instantiate(DefaultObstacleBallPrefab, RandomPostition(), Quaternion.identity) as GameObject;

            obstacleBall.GetComponent<ObstacleBall>().ShootBall();
        }
    }

    /// <summary>
    ///     Returns a random position on the board.
    /// </summary>
    /// <returns>The postition.</returns>
    private Vector3 RandomPostition()
    {
        // Gets a random position on the x and y axis between a number range
        return new Vector3(Random.Range(-620f, 620f), Random.Range(-230f, 340f), 0);
    }
}
