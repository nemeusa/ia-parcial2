using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : State
{
    FSM<TypeFSM> _fsm;
    Enemy _enemy;

    public SearchingState(FSM<TypeFSM> fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
    }
}

