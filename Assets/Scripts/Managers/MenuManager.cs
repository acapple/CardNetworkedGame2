using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    internal static MenuManager MM;

    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private LobbyMenu lobbyMenu;

    internal delegate void leaveMenu();

    private void Awake()
    {
        if (MM == null)
        {
            MM = this;
        } else
        {
            Destroy(this);
            return;
        }
    }

    internal void GoToMainMenu(leaveMenu leaving)
    {
        leaving();
        mainMenu.gameObject.SetActive(true);
    }

    internal void GoToLobby(leaveMenu leaving)
    {
        leaving();
        lobbyMenu.gameObject.SetActive(true);
    }

    internal void GoToGame(leaveMenu leaving)
    {
        leaving();
        //Game scene doesn't exist yet...
    }



    internal void leavingLobby()
    {
        lobbyMenu.gameObject.SetActive(false);
    }

    internal void leavingMainMenu()
    {
        mainMenu.gameObject.SetActive(false);
    }

    internal void leavingGame()
    {
        //Game doesn't exist yet...
    }
}
