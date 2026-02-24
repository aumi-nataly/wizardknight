using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private StateMachine _m;
    private Enemy _enemy;
    private float attackDistance = 5f;

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
        _enemy.StopAttack();
    }

    public void Tick()
    {
        _enemy.Attack();

        if (!_enemy.IsPlayerHereForAwakening(attackDistance))
        {
            _m.StateChange(new ApproachState(_m, _enemy));
        }
    }
}
