using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float lifeTime = 2f;

    void Awake()
    {
 
    }

    private void Start()
    {
        // Запускаем таймер самоуничтожения
        Invoke("DestroyBall", lifeTime);
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

        Debug.Log("ранил");

        FireBallManager.instance.ReturnToPool(this);

        // Отменяем таймер, если столкнулись с врагом
        CancelInvoke("DestroyBall");
    }

    private void DestroyBall()
    {
        FireBallManager.instance.ReturnToPool(this);
    }
}
