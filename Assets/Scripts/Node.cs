using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbords = new List<Node>();
    int _x, _y;
    Grid _grid;
    public bool Block { get; private set; }
    public int Cost { get; private set; }

    [SerializeField] TMP_Text _text;
    public void Initialize(Grid grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        ModifyCost(1);
    }

    public List<Node> Neighbords
    {
        get
        {
            if (neighbords.Count > 0)
                return neighbords;

            var right = _grid.GetNode(_x + 1, _y);
            if (right != null)
                neighbords.Add(right);


            var left = _grid.GetNode(_x - 1, _y);
            if (left != null)
                neighbords.Add(left);


            var up = _grid.GetNode(_x, 1 + _y);
            if (up != null)
                neighbords.Add(up);


            var down = _grid.GetNode(_x, _y - 1);
            if (down != null)
                neighbords.Add(down);



            return neighbords;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ModifyCost(Cost + 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ModifyCost(Cost - 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.SetStartNode(this);
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.instance.SetGoalNode(this);
        }

        if (Input.GetMouseButtonDown(2))
        {
            Block = !Block;

            if (Block)
            {
                GetComponent<MeshRenderer>().material.color = Color.gray;
                gameObject.layer = 6;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = Color.white;
                gameObject.layer = 0;
            }
        }
    }

    void ModifyCost(int newCost)
    {
        Cost = Mathf.Clamp(newCost, 1, 50);

        _text.text = Cost.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (var item in neighbords)
        {
            Gizmos.DrawLine(transform.position, item.transform.position);
        }
    }
}