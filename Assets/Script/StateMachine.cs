using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private IState _current;

    public void StateChange(IState state)
    {
        _current?.Exit();
        _current = state;
        _current.Enter();
    }

    public void Tick()
    { 
        _current.Tick();
    }
}
