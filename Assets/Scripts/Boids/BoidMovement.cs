using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [SerializeField] float _maxVelocity;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _prediction;
    [SerializeField] float _radiusArrive;
    [SerializeField] float _rayDistance;
    [SerializeField] LayerMask _wallLayer;
    public Vector3 Velocity { get; private set; }
    public float MaxVelocity => _maxVelocity;
    public float MaxSpeed => _maxSpeed;

    private void Start()
    {
        ApplyRandomForce();
    }

    private void Update()
    {
        AvoidWall();
        ApplyMovement();
    }

    public void ApplyRandomForce()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
        randomDirection.y = 0f;
        AddForce(randomDirection);
    }

    public void AddForce(Vector3 dir)
    {
        Vector3 desiredVelocity = Vector3.ClampMagnitude(Velocity + dir, _maxVelocity);
        Velocity = Vector3.Lerp(Velocity, desiredVelocity, Time.deltaTime * 5f);

        if (Velocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(Velocity.x, 0, Velocity.z);
            if (direction.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void ApplyMovement()
    {
        if (Velocity != Vector3.zero)
        {
            Vector3 flatVel = new Vector3(Velocity.x, 0, Velocity.z);
            if (flatVel.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(flatVel);
        }

        transform.position += Velocity * Time.deltaTime;
    }

    public void AvoidWall()
    {
        Ray ray = new Ray(transform.position, Velocity.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, _wallLayer))
        {
            Vector3 reflectDir = Vector3.Reflect(Velocity.normalized, hit.normal);
            reflectDir.y = 0f; 
            AddForce(reflectDir * _maxVelocity);
            Debug.DrawRay(transform.position, reflectDir * _rayDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Velocity.normalized * _rayDistance, Color.green);
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.y = 0f;
        desired.Normalize();
        return CalculateSteering(desired);
    }

    public Vector3 Flee(Vector3 target)
    {
        return -Seek(target);
    }

    public Vector3 Evade(Vector3 hunterPosition, Vector3 hunterVelocity)
    {
        var predictedPosition = hunterPosition + hunterVelocity * _prediction;
        predictedPosition.y = 0f;
        return Flee(predictedPosition);
    }

    public Vector3 Arrive(Vector3 target)
    {
        var dist = Vector3.Distance(transform.position, target);

        if (dist > _radiusArrive)
        {
            return Seek(target);
        }

        var desired = target - transform.position;
        desired.y = 0f;
        desired.Normalize();
        desired *= _maxVelocity * (dist / _radiusArrive);

        return CalculateSteering(desired);
    }

    public Vector3 CalculateSteering(Vector3 desired)
    {
        if (desired == Vector3.zero) return desired;

        desired = desired.normalized * _maxVelocity;
        Vector3 steering = desired - Velocity;
        return Vector3.ClampMagnitude(steering, _maxSpeed);
    }
}
