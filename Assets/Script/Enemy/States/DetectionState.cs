using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : IState
{
    private StateMachine _m;
    private Enemy _enemy;

    public DetectionState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.Detection(true);
    }

    public void Exit()
    {
        _enemy.Detection(false);
    }

    public void Tick()
    {
        if (!_enemy.IsPlayerHereForAwakening())
        {
            _m.StateChange(new IdleState(_m, _enemy));
        }
    }
}
