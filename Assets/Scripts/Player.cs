using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Coroutine _CoroutinePath;
    public void SetPath(List<Node> path)
    {
        if (path.Count <= 0) return;

        if (_CoroutinePath != null)
            StopCoroutine(_CoroutinePath);

        _CoroutinePath = StartCoroutine(CoroutinePath(path));
    }

    IEnumerator CoroutinePath(List<Node> path)
    {
        transform.position = path[0].transform.position;
        path.RemoveAt(0);

        while (path.Count > 0)
        {
            var dir = path[0].transform.position - transform.position;
            transform.forward = dir;
            transform.position += transform.forward * speed * Time.deltaTime;

            if (dir.magnitude <= 0.5f)
            {
                path.RemoveAt(0);
            }

            yield return null;
        }

        _CoroutinePath = null;
    }
}
