using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] playerPositions;
    //public Material[] playerMaterial;

    public static event System.Action<Player> OnNewPlayerEntered;
    public static event System.Action<Player> OnPlayerLeft;
    public static event System.Action OnMyPlayerLeft;

    private List<int> openPositions = new List<int> { 0, 1 };

    void Start()
    {
        int index = 1;



        for (int i = 0; i < openPositions.Count; i++)
        {
            if (!Gamedata.occupiedPos.Contains(openPositions[i]))
            {
                index = openPositions[i];
                Debug.Log("open position " + openPositions[i]);
                break;
            }
            else
            {
                Debug.Log("position occupied " + openPositions[i]);
            }
        }

        //if (PhotonNetwork.PlayerList.Length == 1)
        //{
        //    index = 0;
        //    PhotonNetwork.Instantiate(playerPrefab.name, playerPositions[index].position, Quaternion.identity);

        //}
        //else if (PhotonNetwork.PlayerList.Length == 2)
        //{
        //    index = 1;
        //    PhotonNetwork.Instantiate(playerPrefab.name, playerPositions[index].position, Quaternion.identity);
        //}

        //index = PhotonNetwork.IsMasterClient ? 0 : 1;

        //Debug.Log("PhotonNetwork.CurrentRoom.PlayerCount " + PhotonNetwork.CurrentRoom.PlayerCount);
        //int index = playerPositions.Length < PhotonNetwork.CurrentRoom.PlayerCount ? 0 : (PhotonNetwork.CurrentRoom.PlayerCount - 1);
        Hashtable hash = new Hashtable();
        hash.Add("index", index);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        PhotonNetwork.Instantiate(playerPrefab.name, playerPositions[index].position, Quaternion.identity);

        //Debug.Log("PhotonNetwork.CurrentRoom.PlayerCount " + PhotonNetwork.CurrentRoom.PlayerCount);
        //int index = playerPositions.Length < PhotonNetwork.CurrentRoom.PlayerCount ? 0 : (PhotonNetwork.CurrentRoom.PlayerCount - 1);
        //PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        Debug.Log("currentr room " + PhotonNetwork.CurrentRoom.Name);

    }



    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        Debug.Log("---On Player Entered Room--");
        OnNewPlayerEntered?.Invoke(newPlayer);
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("---On Left Room--");
        OnMyPlayerLeft?.Invoke();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("---On Player Left Room--");
        OnPlayerLeft?.Invoke(otherPlayer);
    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        Debug.Log("---On Master Client Switched--");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("--On Disconnect--");
        SceneManager.LoadScene(Gamedata.StartSceneName);
    }


    
}
