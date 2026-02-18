using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private StateMachine _m;
    private Enemy _enemy;

    public IdleState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }


    public void Enter()
    {
        //сделать активным и взять из пула
        Debug.Log("IdleState Enter");
    }

    public void Exit()
    {
        //звук пробуждения
        Debug.Log("IdleState Exit");
    }

    public void Tick()
    {
        //_enemy.Walk();

        //if (_enemy.IsPlayerHere())
        //{
        //    _m.SetState(new CatchState(_m, _enemy));
        //}
    }
}
