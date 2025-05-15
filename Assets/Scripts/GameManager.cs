using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    }
}
