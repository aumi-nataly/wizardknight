using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitted : MonoBehaviour
{

    [SerializeField]
    private float TrapHit;

    [SerializeField]
    private float EnemyHit;

    public event Action<float> PlayerGetHitted;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            PlayerGetHitted?.Invoke(TrapHit);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerGetHitted?.Invoke(EnemyHit);
        }
    }
}
