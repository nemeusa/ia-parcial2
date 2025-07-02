using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningPatrolState : State
{
    FSM<TypeFSM> _fsm;
    Enemy _enemy;

    public ReturningPatrolState(FSM<TypeFSM> fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log("Returning to patrol points...");
    }

    public void OnUpdate()
    {
    }

    public void OnExit()
    {
    }
}
