using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : IState
{
    private StateMachine _m;
    private Enemy _enemy;
    private float detectionDistance = 15f;
    private float attackDistance = 5f;

    public ApproachState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }

    public void Enter()
    {
        Debug.Log("ApproachState Enter");
    }

    public void Exit()
    {
        Debug.Log("ApproachState Exit");
    }

    public void Tick()
    {
        _enemy.DirectionOfMovement();

        if (_enemy.IsGrounded())
        {
            _enemy.Run();
        }

        if (!_enemy.IsPlayerHereForAwakening(detectionDistance))
        {
            _m.StateChange(new IdleState(_m, _enemy));
        }

        if (_enemy.IsPlayerHereForAwakening(attackDistance))
        {
            _m.StateChange(new AttackState(_m, _enemy));
        }
    }
}
