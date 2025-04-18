using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{

    [SerializeField] Vector3 _velocity;

    public Vector3 Velocity => _velocity;
    void Start()
    {
        GameManager.Instance.hunter = this;
    }

}
