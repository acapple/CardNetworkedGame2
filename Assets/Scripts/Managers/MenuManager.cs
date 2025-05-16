using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    internal static MenuManager MM;

    [SerializeField]
    LobbyMenu lobbyMenu;

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

    internal void disableLobby()
    {
        lobbyMenu.gameObject.SetActive(false);
    }
}
