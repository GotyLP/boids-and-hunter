using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoidMovement), typeof(BoidPerception))]
public class BoidFlocking : MonoBehaviour
{
    private BoidMovement _movement;
    private BoidPerception _perception;
    private Boid _boid;
    [SerializeField] private float _maxForce = 3f;


    void Awake()
    {
        _movement = GetComponent<BoidMovement>();
        _perception = GetComponent<BoidPerception>();
        _boid = GetComponent<Boid>();
    }

    public void ApplyFlocking()
    {
        Debug.Log("Boid ejecuta flocking");

        var alignforce = Aligment(GameManager.Instance.totalBoids, _perception.AlignRadius) * GameManager.Instance.weightAligment;
        var cohesionforce = Cohesion(GameManager.Instance.totalBoids, _perception.AlignRadius) * GameManager.Instance.weightCohesion;
        var separationforce = Separation(GameManager.Instance.totalBoids,_perception.SeparationRadius) * GameManager.Instance.weightSeparation;

        _boid.AddForce(alignforce);
        _boid.AddForce(cohesionforce);
        _boid.AddForce(separationforce);

    }


    public Vector3 Separation(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;

        foreach (Boid boid in boids)
        {
            var dir = boid.transform.position - transform.position;
            if (dir.magnitude > radius || boid == this) continue;

            desired -= dir;
        }

        if (desired == Vector3.zero) return desired;

        desired.Normalize();
        desired *= _movement.MaxVelocity;

        var steering = desired - _movement.Velocity;
        steering = Vector3.ClampMagnitude(steering, _movement.MaxSpeed);

        return steering;
    }


    public Vector3 Aligment(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (Boid boid in boids)
        {
            if (boid == _boid) continue;

            if (Vector3.Distance(transform.position, boid.transform.position) <= radius)
            {
                desired += boid.GetVelocity();
                count++;
            }

        }

        if (count <= 0) return desired;

        desired /= count;
        desired.Normalize();
        desired *= _movement.MaxVelocity;

        var steering = desired - _movement.Velocity;
        steering = Vector3.ClampMagnitude(steering, _movement.MaxSpeed);

        return steering;
    }

    public Vector3 Cohesion(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (Boid boid in boids)
        {
            if (boid == _boid) continue;

            if (Vector3.Distance(transform.position, boid.transform.position) <= radius)
            {
                desired += boid.transform.position;
                count++;
            }

        }

        if (count <= 0) return desired;

        desired /= count;
        desired -= transform.position;

        desired.Normalize();
        desired *= _movement.MaxVelocity;

        var steering = desired - _movement.Velocity;
        steering = Vector3.ClampMagnitude(steering, _movement.MaxSpeed);

        return steering;
    }
}

