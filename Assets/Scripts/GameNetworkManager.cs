using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// General Game network management information
/// </summary>
public class GameNetworkManager : MonoBehaviour
{
    internal enum transports { netcode, facepunch }


    [Header("Variables")]
    int maxPlayers = 4;

    [Header("References")]
    //NetworkSwapper.transports transport;
    [SerializeField]
    private UnityNetworkManager UNM;
    [SerializeField]
    private FacepunchNetworkManager FPNM;

    internal static GameNetworkManager GNM;

    internal transports transport { get; private set; }

    /// <summary>
    /// Awake is only to be used to set singletons
    /// </summary>
    private void Awake()
    {
        if (GNM == null)
        {
            GNM = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    /// <summary>
    /// Sets up everything to do with the networks.
    /// </summary>
    internal void init()
    {
        string transportName = NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name;
        if (transportName == "FacepunchTransport") transport = transports.facepunch;
        else if (transportName == "UnityTransport") transport = transports.netcode;
        if (transport == transports.facepunch) FPNM.init();
    }


    /// <summary>
    /// Starts a lobby as host
    /// </summary>
    public void startLobby()
    {
        if (transport == transports.facepunch)
        {
            FPNM.StartHost(maxPlayers);
        } else
        {
            UNM.StartHost(maxPlayers);
        }
    }


    public void joinLobby()
    {
        if (transport == transports.facepunch)
        {
            //FPNM.StartClient();
            Debug.LogWarning("Tried to join a steam game through the join button. Not yet implemented.");
        }
        else
        {
            UNM.StartClient();
        }
    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 200));
        serverStatusLabels();
        GUILayout.EndArea();
    }


    private void serverStatusLabels()
    {
        string mode;
        if (NetworkManager.Singleton.IsHost)
            mode = "Host";
        else if (NetworkManager.Singleton.IsServer)
            mode = "Server";
        else if (NetworkManager.Singleton.IsConnectedClient)
            mode = "Client - Connected";
        else
            mode = "Unknown";
        GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);

        if (NetworkManager.Singleton.IsServer)
        {
            GUILayout.Label("# Connected: " + NetworkManager.Singleton.ConnectedClients.Count);
        }

        if (NetworkManager.Singleton.IsConnectedClient)
        {

        }
    }
}
