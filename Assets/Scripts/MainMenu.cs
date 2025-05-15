using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject lobby;

    public void startLobby()
    {
        GameNetworkManager.GNM.startLobby();
        moveToGameLobby();
    }

    public void JoinSelf()
    {
        GameNetworkManager.GNM.joinLobby();
        moveToGameLobby();
    }

    public void moveToGameLobby()
    {
        lobby.SetActive(true);
        gameObject.SetActive(false);
    }
}
