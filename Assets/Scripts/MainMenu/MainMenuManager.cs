using System;
using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    // The GUI skin for the menu
    public GUISkin MenuSkin;

    public Texture2D LoadingTexture = null;

	// Use this for initialization
    void Awake()
	{
	    var mainMenu = gameObject.AddComponent<MenuMain>();

	    mainMenu.MainMenuSkin = MenuSkin;

	    mainMenu.LoadingTexture = LoadingTexture;

        // Event listener
        mainMenu.OnSinglePlayerClick += OnSinglePlayerClick;
	}

    void OnSinglePlayerClick()
    {
        Destroy(gameObject.GetComponent<MenuMain>());

        var menuPlayerSettings = gameObject.AddComponent<MenuPlayerSettings>();

        menuPlayerSettings.MainMenuSkin = MenuSkin;

        // Event listener
        menuPlayerSettings.OnStartClick += OnStartClick;
    }

    private void OnStartClick(MenuPlayerSettings menuPlayerSettings)
    {
        if (!String.IsNullOrEmpty(menuPlayerSettings.PlayerName))
        {
            // Set the global player preference value
            PlayerPrefs.SetFloat("mouseSense", menuPlayerSettings.MouseSenseValue);

            PlayerPrefs.SetString("playerName", menuPlayerSettings.PlayerName);

            Application.LoadLevel(1);
        }

    }
}
