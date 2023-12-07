using System;
using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Object;
using UnityEngine;

public class ClientServerManager : NetworkBehaviour
{
    [SerializeField] private bool isServer;

    private void Awake()
    {
        if (isServer) InstanceFinder.ServerManager.StartConnection();
        else InstanceFinder.ClientManager.StartConnection();
    }
}
