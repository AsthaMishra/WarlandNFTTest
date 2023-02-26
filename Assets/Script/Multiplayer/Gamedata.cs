using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata : MonoBehaviour
{
    public const string GameSceneName = "ThirdPersonScene";
    public const string StartSceneName = "StartScene";
    public const int MaxPlayer = 2;
    public static List<int> occupiedPos = new List<int>();

    public static Photon.Realtime.Player myPlayer;
}
