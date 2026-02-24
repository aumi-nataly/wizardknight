using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float Speed;


    void Awake()
    {
 
    }

    public void DirectionBall(float dir)
    {
        Speed = dir * Speed;
    }

    void Update()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy"))
        { return; }

        Debug.Log("ранил");

       // ReturnToPool();
    }
}
