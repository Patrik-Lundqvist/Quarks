using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float currentTime;
	private bool _active = false;

	/// <summary>
	/// Gets the current time.
	/// </summary>
	/// <value>The current time.</value>
	public float CurrentTime
	{
		get { return currentTime; }
	}

	/// <summary>
	/// Gets the current time stamp.
	/// </summary>
	/// <value>The current time stamp.</value>
	public string CurrentTimeStamp
	{
		get { return string.Format("{0:0.0}", currentTime); }
	}

	// Use this for initialization
	void Start () 
	{
		currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_active)
			currentTime += Time.deltaTime;
	}

	public bool HasPassed(float time)
	{
		if(currentTime > time)
		{
			return true;
		}

		return false;
	}

	public void Reset()
	{
		currentTime = 0;
	}

	public void StartTimer()
	{
		_active = true;
	}

	public void StopTimer()
	{
		_active = false;
	}

}
