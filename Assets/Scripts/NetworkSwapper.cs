using Netcode.Transports.Facepunch;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

/*
[ExecuteInEditMode]
public class NetworkSwapper : MonoBehaviour
{
    [SerializeField] [Tooltip("Select what transport to use here.")]
    private GameNetworkManager.transports transport;


    [Header("References")]
    [SerializeField]
    private FacepunchTransport facepunch;
    [SerializeField]
    private FacepunchNetworkManager facepunchNetworkManager;
    [SerializeField]
    private UnityTransport unityTransport;
    [SerializeField]
    private UnityNetworkManager unityNetworkManager;
    [SerializeField]
    private NetworkManager networkManager;


    void Update()
    {
        switch(transport)
        {
            case GameNetworkManager.transports.netcode:
                networkManager.NetworkConfig.NetworkTransport = unityTransport;
                facepunch.enabled = false;
                facepunchNetworkManager.enabled = false;
                unityTransport.enabled = true;
                unityNetworkManager.enabled = true;
                break;
            case GameNetworkManager.transports.facepunch:
                networkManager.NetworkConfig.NetworkTransport = facepunch;
                facepunch.enabled = true;
                facepunchNetworkManager.enabled = true;
                unityTransport.enabled = false;
                unityNetworkManager.enabled = false;
                break;
        }
    }

}

*/