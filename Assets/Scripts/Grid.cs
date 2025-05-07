using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<Node> _neighbords = new List<Node>();

    public Node prefab;
    Node[,] _grid;
    public int width, height;
    public float Offset;

    private void Start()
    {
        _grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var node = Instantiate(prefab);
                node.transform.position = new Vector3(x * Offset, 0, y * Offset);
                node.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                _grid[x, y] = node;
                node.Initialize(this, x, y);
            }
        }
    }

    public Node GetNode(int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height)
            return null;
        return _grid[x, y];   
    }
    public int variable { get { return 10; } set { } }
    public List<Node> Neighbords
    {
        get
        {
            if (_neighbords.Count > 0)
                return _neighbords;
            
            return _neighbords;
        }
    }
}
