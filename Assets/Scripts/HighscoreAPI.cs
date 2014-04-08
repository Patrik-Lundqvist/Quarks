using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Security.Cryptography;
using MiniJSON;

/// <summary>
/// Get and post highscore to the API (HMAC authentication)
/// </summary>
public class HighscoreAPI : MonoBehaviour {

    // Singleton
	public static HighscoreAPI Instance = null;

    // API URL
    public string ApiURL;

    // Private key for the api
    public string PrivateKey;

    // Public key for the api
    public string PublicKey;

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
	}

    /// <summary>
    /// Parses the json highscore result and returns a list of highscores
    /// </summary>
    /// <param name="jsonResults"></param>
    /// <returns></returns>
	private List<Highscore> ParseHighscoreResults(string jsonResults) {

        // Create a list of highscores
		var highscoreDataList = new List<Highscore>();
		
        // Deserialize the json result to a dictionary
		var recievedObject = (IDictionary) Json.Deserialize(jsonResults);

        // Check if there are any highscores
		if(recievedObject["highscores"] != null)
		{
            // Get all highscores dictionaries
			var highscores = (IList) recievedObject["highscores"];
			
            // For each dictionary, add a highscore object to the list
			foreach (IDictionary highscore in highscores) {

                // Create a new highscore
				var highscoreData = new Highscore();	
		       
                // Set the highscore name
				highscoreData.Name = highscore["username"] as string;
                
                // Get the score
				var score = highscore["score"] as String;
                
                // Set the parsed score
				highscoreData.Score = Decimal.Parse(score);
				
                // Add the highscore to list
				highscoreDataList.Add(highscoreData);
			} 
		}

		return highscoreDataList;
	}

    /// <summary>
    /// Get highscore records
    /// </summary>
    /// <param name="records"></param>
    /// <param name="callback"></param>
	public void GetHighscores(int records, Action<bool,List<Highscore> > callback) 
	{
		StartCoroutine(Get_HighscoreCoroutine("/highscore/top/" + records.ToString(), callback));
	}

    /// <summary>
    /// Post a new highscore
    /// </summary>
    /// <param name="highscore"></param>
    /// <param name="callback"></param>
	public void PostHighscore(Highscore highscore, Action<bool> callback) 
	{	
        // Create a new dictionary
		var dict = new Dictionary<string, object>();

        // Add the username and score
		dict.Add("username",highscore.Name);
		dict.Add("score",highscore.Score);

        // Serialize the dictionary to json
		var data = Json.Serialize(dict);

        // Start a coroutine that sends the json to the api-server
		StartCoroutine(Post_Coroutine("/highscore/new", data, callback));
	}

    /// <summary>
    /// Send the json data to the API-server
    /// </summary>
    /// <param name="urlSuffix"></param>
    /// <param name="data"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
	private IEnumerator Post_Coroutine(string urlSuffix, string data, Action<bool> callback) 
	{
		var form = new WWWForm();

        // Add the post data
		form.AddField("data", data);

		var query = new WWW(GenerateUrl(urlSuffix,"data=" + data),form);
		yield return query;


		callback(query.error == null);
	}

    /// <summary>
    /// Get the json data from the API-server
    /// </summary>
    /// <param name="urlSuffix"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
	private IEnumerator Get_HighscoreCoroutine(string urlSuffix, Action<bool,List<Highscore> > callback)
	{

	    WWW query;
	    using (query = new WWW(GenerateUrl(urlSuffix)))
	    {
	        yield return query;

	        if (string.IsNullOrEmpty(query.error))
	        {
	            callback(true, ParseHighscoreResults(query.text));
	        }
	        else
	        {
	            callback(false, null);
	        }
	    }
	}

    /// <summary>
    /// Generates a correct url for the api queries
    /// </summary>
    /// <param name="urlSuffix"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private string GenerateUrl(string urlSuffix, string data = "")
	{
        return "http://" + ApiURL + urlSuffix + "?sig=" + CreateSignature(urlSuffix + data + PrivateKey, PrivateKey) + "&key=" + PublicKey;
	}

    /// <summary>
    /// Creates a valid signature based on the posted data with the use of the private key
    /// </summary>
    /// <param name="message"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
	private string CreateSignature(string message, string secret)
	{
        // Set the correct encoding
		var encoding = new System.Text.ASCIIEncoding();
        
        // Set the private key
		var keyByte = encoding.GetBytes(secret);

        // Set the data
		var messageBytes = encoding.GetBytes(message);

        // Use SHA-256 to hash the signature
		using (var hmacsha256 = new HMACSHA256(keyByte))
		{
			var hashmessage = hmacsha256.ComputeHash(messageBytes);

            // Return a signature with URL-escaped characters
			return  WWW.EscapeURL(Convert.ToBase64String(hashmessage));
		}
	}
	
}