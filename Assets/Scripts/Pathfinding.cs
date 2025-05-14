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

            foreach(var node in current._neighbords)
            {
                if (!cameFrom.ContainsKey(node))
                {
                    cameFrom.Add(node, current);
                    frontier.Enqueue(node);
                }
            }
        }
        return new List<Node>();
    }
}
