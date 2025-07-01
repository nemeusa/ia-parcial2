using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Pathfinding pathfinding;
    public Node start, end;
    public Player player;
    public Grid grid;
    public LayerMask layerMask;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (start != null && end != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                grid.ResetNodes();
                var path = pathfinding.CalculateBFS(start, end);

                player.SetPath(path);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                grid.ResetNodes();
                var path = pathfinding.CalculateDijkstra(start, end);

                player.SetPath(path);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                grid.ResetNodes();
                var path = pathfinding.CalculateGreedyBFS(start, end);

                player.SetPath(path);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                grid.ResetNodes();
                var path = pathfinding.CalculateAStar(start, end);

                player.SetPath(path);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                grid.ResetNodes();
                var path = pathfinding.CalculateTheta(start, end);

                player.SetPath(path);
            }

        }
    }
    public void SetStartNode(Node node)
    {
        if (start != null)
            start.GetComponent<MeshRenderer>().material.color = Color.white;

        start = node;
        start.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void SetGoalNode(Node node)
    {
        if (end != null)
            end.GetComponent<MeshRenderer>().material.color = Color.white;


        end = node;
        start.GetComponent<MeshRenderer>().material.color = Color.green;
    }


    public bool InLineOfSight(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, layerMask);
    }
}
