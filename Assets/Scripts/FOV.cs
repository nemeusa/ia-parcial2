using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public Transform characterTarget;

    private void Update()
    {
        if (InFOV(characterTarget))
            GetComponent<MeshRenderer>().material.color = Color.red;
        else
            GetComponent<MeshRenderer>().material.color = Color.white;

    }
    public bool InFOV(Transform target)
    {
        var dir = target.position - transform.position;

        if (dir.magnitude <= viewRadius)
        {
            if (Vector3.Angle(transform.forward, dir) <= viewAngle * 0.5f)
            {
                return GameManager.instance.InLineOfSight(transform.position, target.position);
            }
        }

        return false;
    }
}
