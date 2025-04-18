using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] float _maxVelocity;
    [SerializeField] float _maxSpeed; 
    [SerializeField] float _radiusSeparation;
    [SerializeField] float _radiusAlign;
    Vector3 _velocity;

    void Start()
    {
        GameManager.Instance.totalBoids.Add(this);

        var randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f) * _maxVelocity);
        AddForce(randomDir);

    }

    void Update()
    {
        Flocking();
        transform.forward = _velocity;
        transform.position += _velocity * Time.deltaTime;
    }

    public void Flocking()
    {
        AddForce(Cohesion(GameManager.Instance.totalBoids, _radiusAlign) * GameManager.Instance.wightCohesion);
        AddForce(Separation(GameManager.Instance.totalBoids, _radiusSeparation) * GameManager.Instance.wightSeparation);
        AddForce(Aligment(GameManager.Instance.totalBoids, _radiusAlign) * GameManager.Instance.wightAligment);
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

        if(desired == Vector3.zero) return desired;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxSpeed);

        return steering; 
    }


    public Vector3 Aligment(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (Boid boid in boids)
        {
            if (boid == this) continue;

            if (Vector3.Distance(transform.position, boid.transform.position) <= radius)
            {
                desired += boid._velocity;
                count++;
            }

        }

        if (count <= 0) return desired;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxSpeed);

        return steering;
    }

    public Vector3 Cohesion(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (Boid boid in boids)
        {
            if (boid == this) continue;

            if (Vector3.Distance(transform.position, boid.transform.position) <= radius)
            {
                desired += boid.transform.position;
                count++;
            }

        }

        if (count <= 0) return desired;

        desired -= transform.position;

        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxSpeed);

        return steering;
    }

    public void AddForce(Vector3 dir)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusSeparation);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusAlign);
    }
}
