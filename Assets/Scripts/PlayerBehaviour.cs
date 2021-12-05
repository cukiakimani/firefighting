using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct CharacterInput : INetworkInput
{
    public float Forward;
    public float Right;
}

public class PlayerBehaviour : NetworkBehaviour
{
    private Animator animator;
    private NetworkCharacterController characterController;

    public override void Spawned()
    {
        if (Object.InputAuthority == Runner.LocalPlayer)
        {
            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput.AddListener(OnInput);
        }

        animator = GetComponent<Animator>();
        characterController = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out CharacterInput input))
        {
            Vector3 moveDirection = Vector3.forward * input.Forward + Vector3.right * input.Right;

            characterController.Move(moveDirection);
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

    }

    private void OnInput(NetworkRunner runner, NetworkInput inputContainer)
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");

        var characterInput = new CharacterInput
        {
            Forward = forward,
            Right = right
        };

        inputContainer.Set(characterInput);
    }
}
