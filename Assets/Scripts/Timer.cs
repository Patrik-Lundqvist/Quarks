using UnityEngine;
using System.Collections;

/// <summary>
/// A generic timer class.
/// </summary>
public class Timer : MonoBehaviour {

	// Stores the current time value
	private float currentTime;

	// Is the timer active
	private bool IsActive = false;

	/// <summary>
	/// Gets the current time of the timer.
	/// </summary>
	/// <value>The current time.</value>
	public float CurrentTime
	{
		get { return currentTime; }
	}

	/// <summary>
	/// Gets the current time stamp in a string.
	/// </summary>
	/// <value>The current time stamp.</value>
	public string CurrentTimeStamp
	{
		get { return string.Format("{0:0.0}", currentTime); }
	}

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start () 
	{
		currentTime = 0;
	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		if(IsActive)
			currentTime += Time.deltaTime;
	}

	/// <summary>
	/// Determines whether this instance has passed the specified time.
	/// </summary>
	/// <returns><c>true</c> if this instance has passed the specified time; otherwise, <c>false</c>.</returns>
	/// <param name="time">Time.</param>
	public bool HasPassed(float time)
	{
		if(currentTime > time)
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Reset the timer
	/// </summary>
	public void Reset()
	{
		IsActive = false;
		currentTime = 0;
	}

	/// <summary>
	/// Starts the timer.
	/// </summary>
	public void StartTimer()
	{
		IsActive = true;
	}

	/// <summary>
	/// Stops the timer.
	/// </summary>
	public void StopTimer()
	{
		IsActive = false;
	}

}
