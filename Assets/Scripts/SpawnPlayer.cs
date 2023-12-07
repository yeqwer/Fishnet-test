using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    // private NetworkManager _networkManager;
    // private LocalConnectionState _clientState = LocalConnectionState.Stopped;

    // private void Start()
    // {
    //     StartClient();
    // }

    public override void OnStartClient() 
    {
        base.OnStartClient();

        PlayerSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void PlayerSpawn(NetworkConnection client = null) 
    {
        GameObject go = Instantiate(_playerPrefab);

        Spawn(go, client);
    }

    // public void StartClient()
    // {
    //     if (_networkManager == null)
    //         return;

    //     if (_clientState != LocalConnectionState.Stopped)
    //         _networkManager.ClientManager.StopConnection();
    //     else
    //         _networkManager.ClientManager.StartConnection();
    // }
}
