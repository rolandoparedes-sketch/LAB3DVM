using JetBrains.Annotations;
using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputs;
    public CharacterController controller;


    public float rotationSpeed = 100f;
    public float moveSpeed = 5f;
    public float gravity = -0.81f;
    public float verticalVelocity = 0f;
    public float JumpForce = 10;

    public float pushForce = 4;

    private bool IsDashing;
    public float dashForce;
    public float dashTimer;
    public int dashDuration;

    [SerializeField] private Vector2 moveInput;


    public Action contenedorDeMetodos;

    private void Awake()
    {
        inputs = new();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;


        inputs.Player.Jump.performed += OnJump;

        inputs.Player.Sprint.performed += OnDash;
    }

    

    void Update()
    {
        //OnMove();
        OnSimpleMove();
    }

   public void OnMove()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = transform.forward * moveSpeed * moveInput.y * Time.deltaTime;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        moveDir.y = verticalVelocity;

        if(IsDashing)
        {
            moveDir = transform.forward * dashForce * (dashTimer/dashDuration) ;

            dashTimer -= Time.deltaTime;

            if(dashTimer <= 0)
            IsDashing = false;
        }

    }
    public void OnSimpleMove()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = transform.forward * moveSpeed * moveInput.y;
        controller.SimpleMove(moveDir);
       
   
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded) return;

        verticalVelocity = JumpForce;
        

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print(hit.gameObject.name);

        Vector3 pushDir = (hit.point - transform.position).normalized;

        if (hit.rigidbody != null && hit.rigidbody.linearVelocity == Vector3.zero)
            hit.rigidbody.AddForce(pushDir * pushForce, ForceMode.Impulse);
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        IsDashing = true;
        dashTimer = dashDuration;
    }
}
