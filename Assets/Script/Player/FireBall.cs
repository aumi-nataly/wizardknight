using System;

using UnityEngine;
using VContainer;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float lifeTime = 2f;

    [SerializeField]
    private float powerFire;

    public static event Action<float, Enemy> FireBallHitted;

    private FireBallManager _fireBallManager;

    [Inject]
    public void Construct(FireBallManager fireBallManager)
    {
        _fireBallManager = fireBallManager;
        Debug.Log($"FireBall Construct: _fireBallManager = {_fireBallManager != null}");
    }

   
    private void OnEnable()
    {
        Invoke("DestroyBall", lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }


    public void DirectionBall(float dir)
    {
        Speed = dir * Speed;
    }

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        { return; }

        Enemy enemy = other.GetComponent<Enemy>();
        FireBallHitted?.Invoke(powerFire, enemy);
        _fireBallManager.ReturnToPool(this);
        

        // Отменяем таймер, если столкнулись с врагом
        CancelInvoke("DestroyBall");
    }

    private void DestroyBall()
    {
        _fireBallManager.ReturnToPool(this);
    }
}
