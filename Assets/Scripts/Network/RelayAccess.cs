using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;

public class RelayAccess : MonoBehaviour
{
    protected int maxPlayer = 4;

    private async void Awake()
    {
        Debug.developerConsoleVisible = true;
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
            string relayJoinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);
            Debug.Log(relayJoinCode);
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
            await RelayService.Instance.JoinAllocationAsync(relayJoinCode);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
