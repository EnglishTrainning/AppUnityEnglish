using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using SFSLitJson;
using System.Xml;
using Sfs2X.Entities;
public class ConnectSmartFox : MonoBehaviour {
	public string ServerIP = "127.0.0.1";
	public int ServerPort = 9933;
	public string configFile = "Scripts/Network/sfs-config";
	public bool UseConfig =false;
	public string ZoneName = "BasicExamples";
	public string RoomName ="The Lobby";
	public string UserName = "";
	SmartFox sfs;
	void Start () {
		sfs = new SmartFox ();
		sfs.ThreadSafeMode = true;
		sfs.AddEventListener (SFSEvent.CONNECTION, OnConnection);

		sfs.AddEventListener (SFSEvent.LOGIN, OnLogin);
		sfs.AddEventListener (SFSEvent.LOGIN_ERROR, OnLoginError);

		sfs.AddEventListener (SFSEvent.CONFIG_LOAD_FAILURE, OnConfigFail);
		sfs.AddEventListener (SFSEvent.CONFIG_LOAD_SUCCESS, OnConfigLoad);

		sfs.AddEventListener (SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);
		sfs.AddEventListener (SFSEvent.ROOM_JOIN, OnJoinRoom);
		sfs.AddEventListener (SFSEvent.PUBLIC_MESSAGE, OnPulbicManager);

		if (UseConfig) {
			Debug.Log (Application.dataPath + "/" + configFile);
			sfs.LoadConfig (Application.dataPath + "/" + configFile);

		} else {
			sfs.Connect (ServerIP, ServerPort);
		}


	}
	void OnConfigLoad(BaseEvent e)
	{
		Debug.Log ("Config File Loaded");
		sfs.Connect (sfs.Config.Host,sfs.Config.Port);
	}
	void OnConfigFail(BaseEvent e)
	{
		Debug.Log ("Fail Config File");
	}
	void OnConnection(BaseEvent e)
	{
		if ((bool)e.Params ["success"]) {
			Debug.Log ("Connect Success");
			if (UseConfig) {
				ZoneName = sfs.Config.Zone;
			}
			sfs.Send(new LoginRequest(UserName,"",ZoneName));
		}
			else
				Debug.Log("Connect faild");
	}
	void OnLogin(BaseEvent e)
	{
		Debug.Log ("Loggin" +e.Params["user"]);

		sfs.Send (new JoinRoomRequest(RoomName));
	}
	void OnJoinRoom(BaseEvent e)
	{
		Debug.Log ("Join Room" + e.Params["room"]);
		sfs.Send (new PublicMessageRequest("Hello smartfox"));
	}
	void OnPulbicManager(BaseEvent e)
	{
		Room room = (Room)e.Params ["room"];
		User sender = (User)e.Params ["sender"];
		Debug.Log ("["+RoomName+"]" + sender.Name+ ":" + e.Params["message"]);
	}
	void OnJoinRoomError(BaseEvent e)
	{
		Debug.Log ("Join Room Error (" +e.Params["errorCode"] + "):" + e.Params["errorMessage"]);
	}
	void OnLoginError(BaseEvent e)
	{
		Debug.Log ("Loggin Error" + e.Params["errorCode"]+ "): "+ e.Params["errorMessage"]);
	}
	// Update is called once per frame
	void Update () {
		sfs.ProcessEvents ();
	}
	void OnApplicationQuit()
	{if(sfs.IsConnected)
		{
			sfs.Disconnect ();
		}
	}
}
