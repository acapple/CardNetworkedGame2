using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;

    public void StartGame()
    {
        if (!GameNetworkManager.isHosting) return;
        GameObject gm = Instantiate(gameManager);
        gm.GetComponent<Unity.Netcode.NetworkObject>().Spawn();
        GameManager.GM.StartGame();
    }
}
