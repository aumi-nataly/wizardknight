using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private StateMachine _m;
    private Enemy _enemy;

    public DeadState(StateMachine m, Enemy enemy)
    {
        _m = m;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.Die();
      //  _enemy.IsDeadAndCanReturnPool(true);
    }

    public void Exit()
    {
       
    }

    public void Tick()
    {
        
    }

    
}
