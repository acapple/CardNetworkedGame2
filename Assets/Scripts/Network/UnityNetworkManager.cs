using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

/// <summary>
/// setting up the Unity Network transport
/// </summary>
public class UnityNetworkManager : MonoBehaviour
{
    internal void init()
    {
        //get a textbox enabled for player to put in goal ip address
    }


    internal void StartHost(int maxPlayers)
    {
        NetworkManager.Singleton.StartHost();
    }

    internal void StartClient()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("127.0.0.1", (ushort)7777);
        NetworkManager.Singleton.StartClient();
    }
}
