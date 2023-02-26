using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMultiplayer : MonoBehaviour
{

    public GameObject[] objectToDisableForOtherPlayer;
    public Photon.Realtime.Player player;
    bool isMine = false;
    private void OnEnable()
    {
        SpawnManager.OnNewPlayerEntered += OnNewPlayerEntered;
        SpawnManager.OnPlayerLeft += OnPlayerLeft;
        SpawnManager.OnMyPlayerLeft += OnMyPlayerLeft;
    }

    private void OnDisable()
    {
        SpawnManager.OnNewPlayerEntered -= OnNewPlayerEntered;
        SpawnManager.OnPlayerLeft -= OnPlayerLeft;
        SpawnManager.OnMyPlayerLeft -= OnMyPlayerLeft;
    }

    public void HandleMultiPlayerObjectState(bool _isMine)
    {
        isMine = _isMine;
        if (!_isMine)
        {
            DisableOtherPlayerComponenets();
        }
    }

    private void OnNewPlayerEntered(Player newPlayer)
    {
        //if (newPlayer != PhotonNetwork.LocalPlayer)
        //{
        //    DisableOtherPlayerComponenets();
        //}
    }

    private void OnPlayerLeft(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMyPlayerLeft()
    {
        if (isMine)
        {
            Destroy(this.gameObject);
        }
    }

    void DisableOtherPlayerComponenets()
    {
        for (int i = 0; i < objectToDisableForOtherPlayer.Length; i++)
        {
            objectToDisableForOtherPlayer[i].SetActive(false);
            Debug.Log("Deactivating");
        }
    }
}
