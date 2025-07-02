using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingState : State
{
    FSM<TypeFSM> _fsm;
    Enemy _enemy;

    public ChasingState(FSM<TypeFSM> fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        Debug.Log("Chasing the player...");
        _enemy.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void OnUpdate()
    {
        Vector3 dir = (_enemy.characterTarget.position - _enemy.transform.position).normalized;
        _enemy.transform.position += dir * (_enemy.speed * 1.5f) * Time.deltaTime;
        if (!_enemy.fov.InFOV(_enemy.fov.characterTarget))
        {
            Debug.Log("Player no detected, switching to chasing state.");
            _fsm.ChangeState(TypeFSM.Returning);
        }
        if (Vector3.Distance(_enemy.transform.position, _enemy.characterTarget.position) <= 0.2f)
            SceneManager.LoadScene("Parcial2");
    }

    public void OnExit()
    {
    }
}
