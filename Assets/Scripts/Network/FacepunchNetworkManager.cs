using UnityEngine;
using Unity.Netcode;
using Steamworks;
using Steamworks.Data;
using Netcode.Transports.Facepunch;
using System;

/// <summary>
/// Setting up the facepunch network transport
/// </summary>
public class FacepunchNetworkManager : MonoBehaviour
{
    private FacepunchTransport transport = null;
    public Lobby? currentLobby { get; private set; } = null;

    public ulong hostId;


    internal void init()
    {
        transport = GetComponent<FacepunchTransport>();
        SteamMatchmaking.OnLobbyCreated += SteamMatchmaking_OnLobbyCreated;
        SteamMatchmaking.OnLobbyEntered += SteamMatchmaking_OnLobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined += SteamMatchmaking_OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave += SteamMatchmaking_OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyInvite += SteamMatchmaking_OnLobbyInvite;
        SteamMatchmaking.OnLobbyGameCreated += SteamMatchmaking_OnLobbyGameCreated;
        SteamFriends.OnGameLobbyJoinRequested += SteamFriends_OnGameLobbyJoinRequest;
    }

    private void OnDestroy()
    {
        SteamMatchmaking.OnLobbyCreated -= SteamMatchmaking_OnLobbyCreated;
        SteamMatchmaking.OnLobbyEntered -= SteamMatchmaking_OnLobbyEntered;
        SteamMatchmaking.OnLobbyMemberJoined -= SteamMatchmaking_OnLobbyMemberJoined;
        SteamMatchmaking.OnLobbyMemberLeave -= SteamMatchmaking_OnLobbyMemberLeave;
        SteamMatchmaking.OnLobbyInvite -= SteamMatchmaking_OnLobbyInvite;
        SteamMatchmaking.OnLobbyGameCreated -= SteamMatchmaking_OnLobbyGameCreated;
        SteamFriends.OnGameLobbyJoinRequested -= SteamFriends_OnGameLobbyJoinRequest;

        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
    }

    private void OnApplicationQuit()
    {
        Disconnected();
    }


    //Accept invite / join friend
    private async void SteamFriends_OnGameLobbyJoinRequest(Lobby lobby, SteamId steamId)
    {
        RoomEnter joinedLobby = await lobby.Join();
        if (joinedLobby != RoomEnter.Success) Debug.Log("Lobby Creation failed");
        else
        {
            currentLobby = lobby;
            Debug.Log("lobby joined");
        }
    }

    private void SteamMatchmaking_OnLobbyGameCreated(Lobby lobby, uint ip, ushort port, SteamId steamId)
    {
        Debug.Log("Lobby Created");
    }

    private void SteamMatchmaking_OnLobbyInvite(Friend steamId, Lobby lobby)
    {
        Debug.Log($"Invite from {steamId.Name}");
    }

    private void SteamMatchmaking_OnLobbyMemberJoined(Lobby lobby, Friend steamId)
    {
        Debug.Log("member join");
    }

    private void SteamMatchmaking_OnLobbyMemberLeave(Lobby lobby, Friend steamId)
    {
        Debug.Log("member leave");
    }

    private void SteamMatchmaking_OnLobbyEntered(Lobby lobby)
    {
        if (NetworkManager.Singleton.IsHost) return;
        StartClient(currentLobby.Value.Owner.Id);
    }

    private void SteamMatchmaking_OnLobbyCreated(Result result, Lobby lobby)
    {
        if (result != Result.OK)
        {
            Debug.Log("Lobby not created");
            return;
        }
        lobby.SetPublic();
        lobby.SetJoinable(true);
        lobby.SetGameServer(lobby.Owner.Id);
        Debug.Log($"Lobby Created {lobby.Owner.Name}");
    }

    public async void StartHost(int maxMembers)
    {
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        NetworkManager.Singleton.StartHost();
        currentLobby = await SteamMatchmaking.CreateLobbyAsync(maxMembers);

    }

    public void StartClient(SteamId sId)
    {
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        transport.targetSteamId = sId;
        if (NetworkManager.Singleton.StartClient())
        {
            Debug.Log("Client has started");
        }
    }

    public void Disconnected()
    {
        currentLobby?.Leave();
        if (NetworkManager.Singleton == null) return;
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        }
        else
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
        }
        NetworkManager.Singleton.Shutdown(true);
        Debug.Log("Disconnected");
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        throw new NotImplementedException();
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        throw new NotImplementedException();
    }

    private void Singleton_OnServerStarted()
    {
        Debug.Log("Host started");
    }
}
