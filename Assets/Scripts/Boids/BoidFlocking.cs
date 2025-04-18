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

        var alignforce = Aligment() * GameManager.Instance.wightAligment;
        var cohesionforce = Cohesion() * GameManager.Instance.wightCohesion;
        var separationforce = Separation() * GameManager.Instance.wightSeparation;

        Vector3 force = separationforce + alignforce + cohesionforce;

        force = Vector3.ClampMagnitude(force, _maxForce);


        _boid.AddForce(alignforce + cohesionforce + separationforce);
        //_boid.AddForce(Cohesion() * GameManager.Instance.wightCohesion);
        //_boid.AddForce(Separation() * GameManager.Instance.wightSeparation);
        //_boid.AddForce(Aligment() * GameManager.Instance.wightAligment);
    }

    private Vector3 Aligment()
    {
        Vector3 desired = Vector3.zero;
        var neighbors = _perception.GetBoidsInRadius(_perception.AlignRadius);

        foreach (var b in neighbors)
            desired += b.GetVelocity();

        return _movement.CalculateSteering(desired);
    }

    private Vector3 Cohesion()
    {
        Vector3 desired = Vector3.zero;
        var neighbors = _perception.GetBoidsInRadius(_perception.AlignRadius);

        foreach (var b in neighbors)
            desired += b.transform.position;

        if (neighbors.Count > 0)
            desired = (desired / neighbors.Count) - transform.position;

        return _movement.CalculateSteering(desired);
    }

    private Vector3 Separation()
    {
        Vector3 desired = Vector3.zero;
        var neighbors = _perception.GetBoidsInRadius(_perception.SeparationRadius);

        foreach (var b in neighbors)
            desired -= (b.transform.position - transform.position);

        return _movement.CalculateSteering(desired);
    }
}

