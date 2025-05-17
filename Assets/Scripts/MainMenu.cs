using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void startLobby()
    {
        GameNetworkManager.GNM.startLobby();
        MenuManager.MM.GoToLobby(MenuManager.MM.leavingMainMenu);
    }

    public void JoinSelf()
    {
        GameNetworkManager.GNM.joinLobby();
        MenuManager.MM.GoToLobby(MenuManager.MM.leavingMainMenu);
    }
}
