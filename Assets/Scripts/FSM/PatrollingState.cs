using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : State
{
    FSM<TypeFSM> _fsm;
    Enemy _enemy;

    List<Node> _path;

    public PatrollingState(FSM<TypeFSM> fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.GetComponent<MeshRenderer>().material.color = Color.white;
        if (_enemy.patrolPoints != null && _enemy.patrolPoints.Count > 1)
        {
            GoToNextPatrol();
        }
    }

    public void OnUpdate()
    {
        Debug.Log("update patrol");
        if (_enemy.fov.InFOV(_enemy.characterTarget))
        {
            _fsm.ChangeState(TypeFSM.Chasing);
        }
    }

    public void OnExit()
    {
        Debug.Log("exit patrol");
        _enemy.StopCoroutine(FollowPath(_path));
    }


    void GoToNextPatrol()
    {
        var current = _enemy.patrolPoints[_enemy.patrolIndex];
        var next = _enemy.patrolPoints[(_enemy.patrolIndex + 1) % _enemy.patrolPoints.Count];

        var path = _enemy.gameManager.pathfinding.CalculateAStar(current, next);
        _path = path;

        if (path.Count > 0)
        {
            if (_enemy.pathRoutine != null)
                _enemy.StopCoroutine(_enemy.pathRoutine);
            _enemy.pathRoutine = _enemy.StartCoroutine(FollowPath(path));
        }

        _enemy.patrolIndex = (_enemy.patrolIndex + 1) % _enemy.patrolPoints.Count;
    }

    IEnumerator FollowPath(List<Node> path)
    {
        _enemy.transform.position = path[0].transform.position;
        path.RemoveAt(0);

        while (path.Count > 0)
        {
            if (_fsm.CurrentStateKey != TypeFSM.Patrolling)
                yield break;

            var dir = path[0].transform.position - _enemy.transform.position;
            _enemy.transform.forward = dir;

            _enemy.transform.position += _enemy.transform.forward * _enemy.speed * Time.deltaTime;

            Debug.Log("patrol");
            if (dir.magnitude <= 0.2f)
                path.RemoveAt(0);

            yield return null;
        }

        yield return new WaitForSeconds(_enemy._waitPoint);
        GoToNextPatrol();
    }

}
