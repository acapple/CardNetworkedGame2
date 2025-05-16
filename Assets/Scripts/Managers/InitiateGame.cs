using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateGame : MonoBehaviour
{
    void Start()
    {
        GameNetworkManager.GNM.init();
        Destroy(gameObject);
    }
}
