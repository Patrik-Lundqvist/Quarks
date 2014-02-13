using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin MainMenuSkin;

	void OnGUI()
	{

		GUI.skin = MainMenuSkin;

		GUI.Label(new Rect(10,10,100,45),"Menu");

		if(GUI.Button(new Rect(10,40,100,45),"Play"))
		{
			Application.LoadLevel(1);
		}

		if(GUI.Button(new Rect(10,95,100,45),"Exit"))
		{
			Application.Quit();
		}
	}
}
