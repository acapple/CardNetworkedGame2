using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    internal static GameManager GM;
    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        } else
        {
            Destroy(this);
            return;
        }
    }


    internal void StartGame()
    {
        if (!GameNetworkManager.isHosting) return;
        StartGameClientRPC();

    }

    [Rpc(SendTo.Everyone)]
    internal void StartGameClientRPC()
    {
        MenuManager.MM.GoToGame(MenuManager.MM.leavingLobby);
    }
}
