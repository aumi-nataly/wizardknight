using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int MaxHealth;

    public int CurrentHealth;

    private StateMachine _m;

    private void Awake()
    {
        _m = new StateMachine();
    }

    private void Start()
    { 
        //_m.StateChange(new)
    }
}
