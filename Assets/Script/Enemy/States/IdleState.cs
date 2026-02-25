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
        _enemy.StopMoved();
        Debug.Log("Вошли в IdleState");
    }

    public void Exit()
    {
        _enemy.SleepOff();
    }

    public void Tick()
    {

        if (_enemy.IsDeading())
        {
            _m.StateChange(new DeadState(_m, _enemy));
        }

        if (_enemy.IsPlayerHereForAwakening(detectionDistance))
        {
            _m.StateChange(new ApproachState(_m, _enemy));
        }
    }


}
