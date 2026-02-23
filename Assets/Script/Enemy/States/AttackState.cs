using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private StateMachine _m;
    private Enemy _enemy;
    private float detectionDistance = 15;

    public AttackState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.GrowlEnemy();
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        
    }
}
