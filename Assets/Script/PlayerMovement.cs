using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float JumpForce;

    [SerializeField]
    public LayerMask groundLayer;

    private Rigidbody2D _rb;
    private PlayerInputAction inputActions;
    private bool AttackPress = false;
    private bool JumpWithGround = false;
    private Vector2 Move;
    private Animator animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputAction();
        GameObject girl = transform.GetChild(0).gameObject;
        animator = girl.GetComponent<Animator>();

        inputActions.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => Move = Vector2.zero;
        inputActions.Player.Attack.performed += ctx => AttackPress = true;
        inputActions.Player.Jump.performed += ctx => Jumped();
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void Start()
    {

    }


    void FixedUpdate()
    {
        IsGrounded();
        JumpAnimation();
        Moved();
        FlipCharacter();
        RunAnimation();
    }

    void Moved()
    {
        _rb.velocity = new Vector2(Speed * Move.x, _rb.velocity.y);
    }

    void Jumped()
    {
        if (JumpWithGround)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
        }
    }

    /// <summary>
    /// Стоит ли персонаж на платформе
    /// </summary>
    /// <returns></returns>
    void IsGrounded()
    {
        JumpWithGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);       
    }

    /// <summary>
    /// увидеть луч в scene
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector2 rayStart = transform.position;
        Vector2 rayDirection = Vector2.down;
        float rayLength = 0.6f;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(rayStart, rayDirection * rayLength);
    }

    void JumpAnimation()
    {
        if (JumpWithGround)
        {
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Jumping", true);
        }
    }

    void RunAnimation()
    {
        if (Move.x!=0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    void FlipCharacter()
    {
        Vector3 scale = transform.localScale;

        if (Move.x > 0 && scale.x < 0)
        {
            // Движение вправо, но объект отражён — исправляем
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (Move.x < 0 && scale.x > 0)
        {
            // Движение влево, но объект не отражён — отражаем
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
