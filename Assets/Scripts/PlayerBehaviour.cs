using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Fusion;
using UnityEngine.UI.ProceduralImage;

public struct CharacterInput : INetworkInput
{
    public float Forward;
    public float Right;
    public float LookUp;
    public float LookRight;
    public NetworkBool Shooting;
}

public class PlayerBehaviour : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private float minFOV = 60f;
    [SerializeField] private float maxFOV = 70f;

    [Space, SerializeField] private float lookSpeed;
    [SerializeField] private float lookUpBound;

    [Space, SerializeField] private ProceduralImage crossHair;
    

    private Animator animator;
    private NetworkCharacterController characterController;

    private float lookingUp;
    private float lookingRight;
    private bool wasShooting;
    
    public override void Spawned()
    {
        if (Object.InputAuthority == Runner.LocalPlayer)
        {
            var events = Runner.GetComponent<NetworkEvents>();
            events.OnInput.AddListener(OnInput);
        }

        animator = GetComponent<Animator>();
        characterController = GetComponent<NetworkCharacterController>();
        Cursor.visible = false;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out CharacterInput input))
        {
            Vector3 forward = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
            Vector3 right = Vector3.ProjectOnPlane(camera.transform.right, Vector3.up);

            Vector3 moveDirection = forward * input.Forward + right * input.Right;

            characterController.Move(moveDirection);

            lookingUp += input.LookUp * lookSpeed * Runner.DeltaTime;
            lookingRight += input.LookRight * lookSpeed * Runner.DeltaTime;
            lookingUp = Mathf.Clamp(lookingUp, -lookUpBound, lookUpBound);
            camera.transform.localRotation = Quaternion.Euler(lookingUp, 0f, 0f);

            transform.rotation = Quaternion.AngleAxis(lookingRight, Vector3.up);

            animator.SetFloat("Speed", moveDirection.magnitude);

            if (!wasShooting && input.Shooting)
            {
                // started shooting
                crossHair.rectTransform.DOSizeDelta(Vector2.one * 50f, 0.3f).SetEase(Ease.OutBack);
                DOVirtual.Float(camera.m_Lens.FieldOfView, maxFOV, 0.3f, f => camera.m_Lens.FieldOfView = f)
                    .SetEase(Ease.OutBack);
            }
            else if (wasShooting && !input.Shooting)
            {
                // stopped shooting
                crossHair.rectTransform.DOSizeDelta(Vector2.one * 10f, 0.3f).SetEase(Ease.OutExpo);
                DOVirtual.Float(camera.m_Lens.FieldOfView, minFOV, 0.3f, f => camera.m_Lens.FieldOfView = f)
                    .SetEase(Ease.OutExpo);
            }

            wasShooting = input.Shooting;
        }
    }

    private void OnInput(NetworkRunner runner, NetworkInput inputContainer)
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        float lookUp = -Input.GetAxis("Mouse Y");
        float lookRight = Input.GetAxis("Mouse X");
        NetworkBool shoot = Input.GetMouseButton(0);

        var characterInput = new CharacterInput
        {
            Forward = forward,
            Right = right,
            LookUp = lookUp,
            LookRight = lookRight,
            Shooting = shoot
        };

        inputContainer.Set(characterInput);
    }
}