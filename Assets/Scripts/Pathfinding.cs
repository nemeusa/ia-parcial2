using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{


    public List<Node> CalculateBFS(Node start, Node goal)
    {
        var frontier = new Queue<Node>();
        frontier.Enqueue(start);


        var cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(start, null);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            current.GetComponent<MeshRenderer>().material.color = Color.blue;


            if (current == goal)
            {
                List<Node> path = new List<Node>();

                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];

                }

                path.Reverse();
                return path;
            }

            foreach (var node in current.neighbords)
            {
                if (!cameFrom.ContainsKey(node) && !node.Block)
                {
                    frontier.Enqueue(node);
                    cameFrom.Add(node, current);
                }
            }
        }

        return new List<Node>();
    }

    public List<Node> CalculateDijkstra(Node start, Node goal)
    {
        var frontier = new PriorityQueue<Node>();
        frontier.Enqueue(start, 0);

        var cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(start, null);
        var costSoFar = new Dictionary<Node, float>();
        costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            current.GetComponent<MeshRenderer>().material.color = Color.blue;
            if (current == goal)
            {
                List<Node> path = new List<Node>();

                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }

                path.Reverse();

                return path;
            }
            foreach (var node in current.Neighbords)
            {
                if (node.Block) continue;

                float newCost = costSoFar[current] + node.Cost;

                if (!cameFrom.ContainsKey(node))
                {
                    frontier.Enqueue(node, newCost);
                    cameFrom.Add(node, current);
                    costSoFar.Add(node, newCost);
                }

                if (costSoFar.ContainsKey(node) && costSoFar[node] > newCost)
                {
                    cameFrom[node] = current;
                    costSoFar[node] = newCost;
                }
            }
        }

        return new List<Node>();
    }

    public List<Node> CalculateGreedyBFS(Node start, Node goal)
    {
        var frontier = new PriorityQueue<Node>();
        frontier.Enqueue(start, 0);

        var cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(start, null);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            current.GetComponent<MeshRenderer>().material.color = Color.blue;
            if (current == goal)
            {
                List<Node> path = new List<Node>();

                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }

                path.Reverse();

                return path;
            }
            foreach (var node in current.Neighbords)
            {
                if (!cameFrom.ContainsKey(node) && !node.Block)
                {
                    var priority = Vector3.Distance(node.transform.position, goal.transform.position);
                    frontier.Enqueue(node, priority);
                    cameFrom.Add(node, current);
                }
            }
        }

        return new List<Node>();
    }

    public List<Node> CalculateAStar(Node start, Node goal)
    {
        var frontier = new PriorityQueue<Node>();
        frontier.Enqueue(start, 0);

        var cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(start, null);
        var costSoFar = new Dictionary<Node, float>();
        costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            current.GetComponent<MeshRenderer>().material.color = Color.blue;
            if (current == goal)
            {
                List<Node> path = new List<Node>();

                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }

                path.Reverse();

                return path;
            }
            foreach (var node in current.Neighbords)
            {
                if (node.Block) continue;

                float newCost = costSoFar[current] + node.Cost;
                float priority = newCost + Vector3.Distance(node.transform.position, goal.transform.position);

                if (!cameFrom.ContainsKey(node))
                {
                    frontier.Enqueue(node, priority);
                    cameFrom.Add(node, current);
                    costSoFar.Add(node, newCost);
                }

                if (costSoFar.ContainsKey(node) && costSoFar[node] > newCost)
                {
                    frontier.Enqueue(node, priority);
                    cameFrom[node] = current;
                    costSoFar[node] = newCost;
                }
            }
        }

        return new List<Node>();
    }

    public List<Node> CalculateTheta(Node start, Node goal)
    {
        var aStar = CalculateAStar(start, goal);

        int current = 0;

        while (current + 2 < aStar.Count)
        {
            if (GameManager.instance.InLineOfSight(aStar[current].transform.position, aStar[current + 2].transform.position))
                aStar.RemoveAt(current + 1);
            else
                current++;
        }
        return aStar;
    }
}