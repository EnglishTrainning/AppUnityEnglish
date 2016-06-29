using UnityEngine;
using System.Collections;
using Parse;
public class test : MonoBehaviour {
	// Use this for initialization
	void Start () {
		ParseObject gameScore = new ParseObject("GameScore");
		gameScore["score"] = 1337;
		gameScore["playerName"] = "HieuLuu";
		Task saveTask = gameScore.SaveAsync();

	}
	// Update is called once per frame
	void Update () {

	}

}
