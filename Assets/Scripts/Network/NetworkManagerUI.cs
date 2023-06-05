using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button createRelayButton;
    [SerializeField] private Button joinRelayButton;
    [SerializeField] private TextMeshProUGUI relayCodeText;
    [SerializeField] private RelayAccess relayAccess;

    private string relayCode;

    private void Awake()
    {
        createRelayButton.onClick.AddListener(() => {
            relayAccess.CreateRelay();});
            
        joinRelayButton.onClick.AddListener(() => {
            relayAccess.JoinRelay(relayCode);});
    }

    public void UpdateRelayCode(string newRelayCode)
    {
        relayCode = newRelayCode;
        relayCodeText.text = newRelayCode;
    }
}