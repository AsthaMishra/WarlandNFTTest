using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviourPunCallbacks
{
    public GameObject connectPanel;
    public GameObject createJoinPanel;

    // Start is called before the first frame update
    void Start()
    {
        connectPanel.SetActive(true);
        createJoinPanel.SetActive(false);
    }


    public void OnClickConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClickCreateJoinRoom()
    {
        Gamedata.myPlayer = PhotonNetwork.LocalPlayer;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = Gamedata.MaxPlayer;
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, null);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("---Connected to server---");

        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("---On Room Created---");
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("---On Room Creation Failed---" + message);
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("---On Joined Lobby--");
        connectPanel.SetActive(false);
        createJoinPanel.SetActive(true);
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        Debug.Log("---On Left Lobby--");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("---On Joined Room--");
        Debug.Log("Total players in room " + PhotonNetwork.CurrentRoom.PlayerCount + " max players " + PhotonNetwork.CurrentRoom.MaxPlayers);
        Gamedata.occupiedPos.Clear();


        foreach (KeyValuePair<int, Photon.Realtime.Player> item in PhotonNetwork.CurrentRoom.Players)
        {


            Debug.Log("key " + item.Key + " data " + item.Value.UserId + "is local " + item.Value.IsLocal + "index " + item.Value.CustomProperties["index"] + " -- " + item.Value.ActorNumber);
            if (item.Value.CustomProperties["index"] != null && (item.Value != PhotonNetwork.LocalPlayer))
                Gamedata.occupiedPos.Add((int)item.Value.CustomProperties["index"]);
        }

        PhotonNetwork.LoadLevel(Gamedata.GameSceneName);
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("---On Joined Random Room Failed--" + message);
    }

}
