using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin MainMenuSkin;
	public float hSliderValue;


	void Start()
	{
		hSliderValue = 1f;
	}
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
		GUI.Label(new Rect(10,140,100,45),hSliderValue.ToString("0.00"));

		hSliderValue = GUI.HorizontalSlider(new Rect(10, 180, 200, 30), hSliderValue, 0.0F, 2.0F);

		PlayerPrefs.SetFloat("mouseSense",hSliderValue);
	}
}
