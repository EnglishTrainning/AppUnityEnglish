using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
public class test : MonoBehaviour {
	 IEnumerable<ParseObject> results;
	// Use this for initialization

	void Start () {
	
		ParseInitializeBehaviour _script = new GameObject("ParseInitializeBehaviour").AddComponent<ParseInitializeBehaviour> ();
		_script.applicationID = "apphieusan";
		_script.dotnetKey = "hieusan";

		ParseClient.Initialize (new ParseClient.Configuration () 
			{
				WindowsKey = "hieusan",
				ApplicationId = "apphieusan",
				Server = "https://battleplanet.herokuapp.com/parse/"
					
			});
	}
	// Update is called once per frame
	void Update () {

	}
	public void clickbutton()
	{
//		Debug.Log (ParseClient.Initialize.);
     
       GetParseFiles();
		ParsePersonal ();
		ParseWellBeing ();
		ParseGallery();
}
void GetParseFiles()
    {
		 ParseObject gameScore = new ParseObject("GameScore");
		 	gameScore["score"] = 1337;
		 	gameScore["playerName"] = "HieuLuu";
		 	Task saveTask = gameScore.SaveAsync();
       
    }	
	public static void ParsePersonal() {
		ParseAnalytics.TrackEventAsync("Personal Views");
		Debug.Log("tracking personal view");
	}

	public static void ParseWellBeing() {
		ParseAnalytics.TrackEventAsync("Well Being Views");
		Debug.Log("tracking well being views");
	}

	public static void ParseGallery() {
		ParseAnalytics.TrackEventAsync("Gallery Views");
		Debug.Log("tracking gallery view");
	}
}
