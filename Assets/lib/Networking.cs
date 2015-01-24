using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {
	public string GameRoom = "Room1"; 

	// Use this for initialization
	public void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
		lastState = PhotonNetwork.connectionStateDetailed;
	}
	
	// Update is called once per frame
	public void Update () {
		if (lastState != PhotonNetwork.connectionStateDetailed) {
			lastState = PhotonNetwork.connectionStateDetailed;
			Debug.Log("Networking changed state to: " + lastState.ToString());
		}
	}

	public void OnJoinedLobby() {
		PhotonNetwork.JoinRoom (GameRoom, true);
	}

	private PeerState lastState; 
}
