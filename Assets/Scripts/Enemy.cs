using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    public float speed = 3f;
    public List<Node> patrolPoints;

    public float _waitPoint;

    public int patrolIndex = 0;
    public Coroutine pathRoutine;

    public  GameManager gameManager;

    public FOV fov;

    public Transform characterTarget;

    void Awake()
    {

        fov = GetComponent<FOV>();

        _fsm = new FSM<TypeFSM>();
        _fsm.AddState(TypeFSM.Patrolling, new PatrollingState(_fsm, this));
        _fsm.AddState(TypeFSM.Chasing, new ChasingState(_fsm, this));
        _fsm.AddState(TypeFSM.Returning, new ReturningPatrolState(_fsm, this));
        _fsm.AddState(TypeFSM.Searching, new SearchingState(_fsm, this));

        _fsm.ChangeState(TypeFSM.Patrolling);
    }

    private void Update()
    {
        _fsm.Execute();
    }

    public Node FindClosestNode()
    {
        Node closest = null;
        float minDist = Mathf.Infinity;
        //foreach (var node in gameManager.pathfinding.GetAllNodes())
        //{
        //    float dist = Vector3.Distance(transform.position, node.transform.position);
        //    if (dist < minDist)
        //    {
        //        minDist = dist;
        //        closest = node;
        //    }
        //}
        return closest;
    }

}

public enum TypeFSM
{
    Patrolling,
    Chasing,
    Returning,
    Searching
}
