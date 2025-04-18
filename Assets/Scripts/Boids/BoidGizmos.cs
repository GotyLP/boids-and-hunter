using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(BoidPerception))]
public class BoidGizmos : MonoBehaviour
{
    private BoidPerception _perception;

    void OnDrawGizmos()
    {
        _perception ??= GetComponent<BoidPerception>();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _perception.SeparationRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _perception.AlignRadius);
    }
}

