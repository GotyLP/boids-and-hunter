using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] float _maxVelocity;
    [SerializeField] float _maxSpeed;

    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        //var randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f) * _maxVelocity);
        //AddForce(randomDir);

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
        randomDirection.y = 0f;
        AddForce(randomDirection);
    }

    public void AddForce(Vector3 dir)
    {
        Velocity = Vector3.ClampMagnitude(Velocity + dir, _maxVelocity);
    }

    public void ApplyMovement()
    {
        //Debug.Log("Aplying movement");
        //Vector3 randomDirection = Random.insideUnitSphere;
        //randomDirection.y = 0f;

        //AddForce(randomDirection);

        //transform.forward = Velocity;
        //transform.position += Velocity * Time.deltaTime;
        
        transform.forward = Velocity;
        transform.position += Velocity * Time.deltaTime;
    }

    public Vector3 CalculateSteering(Vector3 desired)
    {
        if (desired == Vector3.zero) return desired;

        desired = desired.normalized * _maxVelocity;
        Vector3 steering = desired - Velocity;
        return Vector3.ClampMagnitude(steering, _maxSpeed);
    }
}

