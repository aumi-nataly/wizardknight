using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private StateMachine _m;
    private Enemy _enemy;
    private float detectionDistance = 15;

    public IdleState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }


    public void Enter()
    {
        Debug.Log("IdleState Enter");
        _enemy.NeedReturning = true;
    }

    public void Exit()
    {
        _enemy.SleepOff();
        Debug.Log("IdleState Exit");
    }

    public void Tick()
    {

        if (_enemy.IsPlayerHereForAwakening(detectionDistance))
        {
            _m.StateChange(new ApproachState(_m, _enemy));
        }
    }


}
