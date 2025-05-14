using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> _neighbords;
    int _x, _y;
    Grid _grid;
    public void Initialize(Grid grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }

    public List<Node> Neighbords
    {
        get
        {
            if (_neighbords.Count > 0)
                return _neighbords;

            var right = _grid.GetNode(_x + 1, _y);
            if (right != null)
                _neighbords.Add(right);


            var left = _grid.GetNode(_x - 1, _y);
            if (left != null)
                _neighbords.Add(left);


            var up = _grid.GetNode(_x, 1 + _y);
            if (up != null)
                _neighbords.Add(up);


            var down = _grid.GetNode(_x, 1 - _y);
            if (down != null)
                _neighbords.Add(down);



            return _neighbords;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SetStartNode(this);
        }     

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.SetGoalNode(this);
        }
    }
}
