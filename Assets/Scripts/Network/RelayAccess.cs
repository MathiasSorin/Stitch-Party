using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;

public class RelayAccess : MonoBehaviour
{
    protected int maxPlayer = 4;

    private async void Awake()
    {
        Debug.developerConsoleVisible = true;
        await UnityServices.InitializeAsync();
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e);
        }
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);

            Debug.Log("RELAY JOIN CODE IS: "+ relayJoinCode);
            GetComponent<NetworkManagerUI>().UpdateRelayCode(relayJoinCode);

            RelayServerData relayServerData = new RelayServerData(relayAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string relayJoinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayJoinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
