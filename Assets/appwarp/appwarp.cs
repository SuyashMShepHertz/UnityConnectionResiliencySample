using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

//using UnityEditor;

using AssemblyCSharp;

public class appwarp : MonoBehaviour 
{
	public string apiKey = "b29f4030aba3b2bc7002c4eae6815a4130c862c386e43ae2a0a092b27de1c5af";
	public string secretKey = "bf45f27e826039754f8dda659166d59ffb7b9dce830ac51d6e6b576ae4b26f7e";
	public string roomid = "1440375425";
	public static string username = "";
	
	public bool useUDP = true;
	
	Listener listen;
	
	void Start () {
		WarpClient.initialize(apiKey,secretKey/*, "54.246.103.117"*/);
		listen = GetComponent<Listener>();
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddLobbyRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		WarpClient.GetInstance().AddUpdateRequestListener(listen);
		WarpClient.GetInstance().AddZoneRequestListener(listen);

		WarpClient.setRecoveryAllowance (60);
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString();
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
	}
	
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
    	}
		WarpClient.GetInstance().Update();
	}
	
	void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(240,10,400,500), listen.getDebug());

		if(GUI.Button(new Rect(10,10,100, 30), "Get State"))
		{
			byte state = WarpClient.GetInstance().GetConnectionState();
			switch(state)
			{
			case WarpConnectionState.CONNECTED:
				listen.onLog("State : Connected");
				break;
			case WarpConnectionState.CONNECTING:
				listen.onLog("State : Connecting");
				break;
			case WarpConnectionState.DISCONNECTED:
				listen.onLog("State : Disconnected");
				break;
			case WarpConnectionState.DISCONNECTING:
				listen.onLog("State : Disconnecting");
				break;
			case WarpConnectionState.RECOVERING:
				listen.onLog("State : Recovering");
				break;
			}
		}

		if(GUI.Button(new Rect(120,10,100, 30), "Disconnect"))
		{
			WarpClient.GetInstance().Disconnect();
		}

		if(GUI.Button(new Rect(10,50,100, 30), "Connect"))
		{
			WarpClient.GetInstance().Connect(username);
		}

		if(GUI.Button(new Rect(120,50,100, 30), "Recover"))
		{
			WarpClient.GetInstance().RecoverConnection();
		}

		if(GUI.Button(new Rect(10,90,100, 30), "Join Room"))
		{
			WarpClient.GetInstance().JoinRoom(roomid);
		}

		if(GUI.Button(new Rect(120,90,100, 30), "Subscribe Room"))
		{
			WarpClient.GetInstance().SubscribeRoom(roomid);
		}

		if(GUI.Button(new Rect(10,130,100, 30), "Send Chat"))
		{
			WarpClient.GetInstance().SendChat("Hi");
		}
	}
	
	/*void OnEditorStateChanged()
	{
    	if(EditorApplication.isPlaying == false) 
		{
			WarpClient.GetInstance().Disconnect();
    	}
	}*/
	
	void OnApplicationQuit()
	{
		//WarpClient.GetInstance().Disconnect();
	}
	
	public void onMsg(string sender, string msg)
	{
		/*
		if(sender != username)
		{
			
		}
		*/
	}
	
	public void onBytes(byte[] msg)
	{	

	}

	public void onUserLeft(string user)
	{

	}
	
}
