using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningPatrolState : State
{
    FSM<TypeFSM> _fsm;
    Enemy _enemy;
    List<Node> _returnPath;

    public ReturningPatrolState(FSM<TypeFSM> fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log("Returning to patrol points...");

        Debug.Log("Returning ENTER");

        // buscar el nodo de patrulla más cercano
        Node nearestPatrolNode = null;
        float minDist = Mathf.Infinity;
        foreach (var patrolNode in _enemy.patrolPoints)
        {
            float dist = Vector3.Distance(_enemy.transform.position, patrolNode.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestPatrolNode = patrolNode;
            }
        }

        if (nearestPatrolNode != null)
        {
            // calcular A*
            var path = _enemy.gameManager.pathfinding.CalculateAStar(
                _enemy.FindClosestNode(), // desde su nodo más cercano
                nearestPatrolNode
            );

            if (_enemy.pathRoutine != null)
                _enemy.StopCoroutine(_enemy.pathRoutine);

            _enemy.pathRoutine = _enemy.StartCoroutine(FollowReturnPath(path));
        }
    }

    public void OnUpdate()
    {
        if (_enemy.fov.InFOV(_enemy.characterTarget))
        {
            _fsm.ChangeState(TypeFSM.Chasing);
        }
    }

    public void OnExit()
    {
        if (_enemy.pathRoutine != null)
            _enemy.StopCoroutine(_enemy.pathRoutine);
    }

    IEnumerator FollowReturnPath(List<Node> path)
    {
        if (path == null || path.Count == 0)
        {
            _fsm.ChangeState(TypeFSM.Patrolling);
            yield break;
        }

        _enemy.transform.position = path[0].transform.position;
        path.RemoveAt(0);

        while (path.Count > 0)
        {
            // abortar si cambia de estado
            if (_fsm.CurrentStateKey != TypeFSM.Returning)
                yield break;

            var dir = path[0].transform.position - _enemy.transform.position;
            _enemy.transform.forward = dir;

            _enemy.transform.position += _enemy.transform.forward * _enemy.speed * Time.deltaTime;

            if (dir.magnitude <= 0.2f)
                path.RemoveAt(0);

            yield return null;
        }

        // cuando llega, vuelve a patrullar
        _fsm.ChangeState(TypeFSM.Patrolling);
    }
}
