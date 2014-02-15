using UnityEngine;
using System.Collections;

/// <summary>
/// Draws and handles the logic for the main menu
/// </summary>
public class MainMenu : MonoBehaviour {

	// The GUI skin for the menu
	public GUISkin MainMenuSkin;

	// Mouse sense slider value
	public float mouseSenseValue;

	/// <summary>
	/// Start the instance.
	/// </summary>
	void Start()
	{
		// Set the starting sense to 1 (read this value of disk in the future)
		mouseSenseValue = 1f;
	}

	/// <summary>
	/// On every updated gui frame
	/// </summary>
	void OnGUI()
	{
		// Set the GUI skin
		GUI.skin = MainMenuSkin;

		// Draw the top menu label
		GUI.Label(new Rect(10,10,100,45),"Menu");

		// Logic for the singleplayer button
		if(GUI.Button(new Rect(10,40,100,45), "Marathon"))
		{
			Application.LoadLevel(1);
		}

		// Logic for the multiplayer button ( not implemented yet )
		if(GUI.Button(new Rect(10,70,100,45), "Multiplayer"))
		{

		}

		// Logic for the exit button
		if(GUI.Button(new Rect(10,100,100,45),"Exit"))
		{
			Application.Quit();
		}

		// Draw the mouse sense slider label
		GUI.Label(new Rect(10,140,100,45),mouseSenseValue.ToString("0.00"));

		// Get the slider value
		mouseSenseValue = GUI.HorizontalSlider(new Rect(10, 180, 200, 30), mouseSenseValue, 0.0F, 2.0F);

		// Set the global player preference value
		PlayerPrefs.SetFloat("mouseSense",mouseSenseValue);
	}
}
