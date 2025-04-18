using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidMovement), typeof(BoidPerception), typeof(BoidFlocking))]
public class Boid : MonoBehaviour
{
    public Node firstNode;

    private BoidMovement _movement;

    void Start()
    {
        GameManager.Instance.totalBoids.Add(this);
        _movement = GetComponent<BoidMovement>();
        firstNode.Execute(this);
    }

    void Update()
    {
        firstNode.Execute(this);
        _movement.ApplyMovement();
    }

    public void AddForce(Vector3 force)
    {
        _movement.AddForce(force);
    }

    public Vector3 GetVelocity() => _movement.Velocity;
}


