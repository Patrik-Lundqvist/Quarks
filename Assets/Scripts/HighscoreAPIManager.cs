using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using HighscoreAPI;

/// <summary>
/// Get and post highscore to the API (HMAC authentication)
/// </summary>
public class HighscoreAPIManager : MonoBehaviour {

    // Singleton
	public static HighscoreAPIManager Instance = null;

    // API URL
    public string ApiURL;

    // Private key for the api
    public string PrivateKey;

    // Public key for the api
    public string PublicKey;

    public string GameVersion;

    public HighscoreAPI.Client Client { get; internal set; }

    /// <summary>
    /// Use this for initialization
	/// </summary>
	void Awake () {

        // Make sure there's only one instance
		if (Instance == null) {
			Instance = this;
		}
		else {
			Debug.LogError("More then one instance of HighscoreAPI");
		}

        // Make sure we have set a private key
        if(string.IsNullOrEmpty(PrivateKey))
            Debug.LogError("Set api private key");
        
        // Make sure we have set a public key
        if (string.IsNullOrEmpty(PublicKey))
            Debug.LogError("Set api public key");
       
        // Make sure we have set an API URL
        if (string.IsNullOrEmpty(ApiURL))
            Debug.LogError("Set api url");

        // Make sure we have set the game version
        if (string.IsNullOrEmpty(GameVersion))
            Debug.LogError("Set game version");

        Client = new Client(PublicKey, PrivateKey, ApiURL, GameVersion);
	}

	
}