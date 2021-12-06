using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct CharacterInput : INetworkInput
{
    public float Forward;
    public float Right;
    public float LookUp;
    public float LookRight;
}

public class PlayerBehaviour : NetworkBehaviour
{
    [SerializeField] private Transform camera;

    [Space, SerializeField] private float lookUpBound;

    private Animator animator;
    private NetworkCharacterController characterController;

    private float lookingUp;
    private float lookingRight;

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
            Vector3 forward = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
            Vector3 right = Vector3.ProjectOnPlane(camera.right, Vector3.up);

            Vector3 moveDirection = forward * input.Forward + right * input.Right;

            characterController.Move(moveDirection);

            lookingUp += input.LookUp;
            lookingRight += input.LookRight;
            lookingUp = Mathf.Clamp(lookingUp, -lookUpBound, lookUpBound);
            camera.localRotation = Quaternion.Euler(lookingUp, 0f, 0f);

            transform.rotation = Quaternion.AngleAxis(lookingRight, Vector3.up);
            
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

    }

    private void OnInput(NetworkRunner runner, NetworkInput inputContainer)
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        float lookUp = -Input.GetAxis("Mouse Y");
        float lookRight = Input.GetAxis("Mouse X");

        var characterInput = new CharacterInput
        {
            Forward = forward,
            Right = right,
            LookUp = lookUp,
            LookRight = lookRight
        };

        inputContainer.Set(characterInput);
    }
}
