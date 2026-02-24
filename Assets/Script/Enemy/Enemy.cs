using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int MaxHealth;

   
    [SerializeField]
    private float Speed;

    [SerializeField]
    private LayerMask groundLayer;

    private GameObject player;

    public int CurrentHealth;

    private StateMachine _m;

    private Animator animator;


    private void Awake()
    {
        _m = new StateMachine();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _m.StateChange(new IdleState(_m, this));

    }

    private void Update()
    {
        _m.Tick();
    }


    /// <summary>
    /// Стоит ли враг на платформе
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        var nV = new Vector2(transform.position.x + Speed, transform.position.y);
        return Physics2D.Raycast(nV, Vector2.down, 0.6f, groundLayer);
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

    /// <summary>
    /// определить в какую сторону бежать за игроком:налево - направо
    /// </summary>
    public void DirectionOfMovement()
    {

        //игрок справа
        if (transform.position.x <= player.transform.position.x)
        {
            Speed = Mathf.Abs(Speed);
        }
        else 
        {
            Speed = -1 * Mathf.Abs(Speed);
        }
    }

    /// <summary>
    /// Рык
    /// </summary>
    public void GrowlEnemy()
    {
        AudioManager.instance.PlayGrowlEnemy();
    }

    /// <summary>
    /// Завершить сон
    /// </summary>
    public void SleepOff()
    {
        AudioManager.instance.PlayDetectionEnemy();
    }

    /// <summary>
    /// Проверка как близок игрок для пробуждения/засыпания/атаки
    /// </summary>
    /// <returns></returns>
    public bool IsPlayerHereForAwakening(float detectionDistance)
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance < detectionDistance;
    }

    public void Run()
    {
        animator.SetBool("RunningEnemy", true);
        FlipCharacter();
        transform.Translate(Speed * Time.deltaTime, 0, 0, Space.World);
    }

    public void StopMoved()
    {
        animator.SetBool("RunningEnemy", false);
    }

    public void StopAttack()
    {
        animator.SetBool("AttackEnemy", false);
    }
    public void Attack()
    {
        animator.SetBool("AttackEnemy", true);
    }

    void FlipCharacter()
    {
        Vector3 scale = transform.localScale;

        if (Speed > 0 && scale.x < 0)
        {
            // Движение вправо, но объект отражён — исправляем
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (Speed < 0 && scale.x > 0)
        {
            // Движение влево, но объект не отражён — отражаем
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}
