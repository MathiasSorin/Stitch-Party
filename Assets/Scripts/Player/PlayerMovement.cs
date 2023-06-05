using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;
    public PlayerInput pi;

    [Header("Movement Variables")]
    public float movementSpeed = 5f;

    private Vector2 movementInput;

    public override void OnNetworkSpawn()
    {
        if(!IsLocalPlayer)
        {
            pi.enabled = false;
        }
    }

    private void Start()
    {
        if(!IsLocalPlayer)
        {
            this.enabled = false;
        }
    }

    //Called on player input
    public void OnMovementInput(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
    }

    [ServerRpc(RequireOwnership = false)]
    public void MovePlayerServerRpc(Vector2 movementInput)
    {
        rb.velocity = new Vector2(movementInput.x*movementSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        MovePlayerServerRpc(movementInput);
    }
}