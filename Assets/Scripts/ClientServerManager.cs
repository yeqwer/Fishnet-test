using FishNet;
using FishNet.Object;
using UnityEngine;

public class ClientServerManager : NetworkBehaviour
{
    [SerializeField] private DevState _devState;

    private void Awake()
    {
        switch (_devState)
        {
            case DevState.BuildSerser:
                InstanceFinder.ServerManager.StartConnection();
                break;

            case DevState.BuildLocalhost:
                InstanceFinder.ServerManager.StartConnection();
                InstanceFinder.ClientManager.StartConnection();
                break;
            case DevState.Play:
                InstanceFinder.ClientManager.StartConnection();
                break;
        }
    }
}

enum DevState
{
    BuildSerser,
    BuildLocalhost,
    Play,
}