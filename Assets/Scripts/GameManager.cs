using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Pathfinding pathfinding;

    public Node start, end;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetButton("Jump") && start != null && end != null)
        {
            var path = pathfinding.CalculateBFS(start, end);

            foreach (var item in path)
            {
                item.GetComponent<MeshRenderer>().material.color = Color.blue;
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
}
