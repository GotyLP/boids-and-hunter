using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public TypeAction typeAction;
    private BoidMovement movement;
    private BoidFlocking flocking;
    private BoidPerception perception;
    public override void Execute(Boid boid)
    {
        switch (typeAction)
        {
            case TypeAction.Arrive:
                Debug.Log("Executed ActionNode Arrive");
                movement = boid.GetComponent<BoidMovement>();
                perception = boid.GetComponent<BoidPerception>();

                var force = movement.Arrive(perception.MostNearbyFood);
                if (force != Vector3.zero)
                    movement.AddForce(force);
                perception.TryConsumeNearbyFood();
                break;
            case TypeAction.Evade:
                movement = boid.GetComponent<BoidMovement>();
                perception = boid.GetComponent<BoidPerception>();
                force = movement.Evade(perception.HunterPosition, perception.HunterVelocity);
                if (force != Vector3.zero)
                    movement.AddForce(force);
                Debug.Log("Executed ActionNode Evade");
                break;
            case TypeAction.Flocking:
                Debug.Log("Executed ActionNode Flocking");
                flocking = boid.GetComponent<BoidFlocking>();
                flocking.ApplyFlocking();
                break;
            case TypeAction.MoveRandom:
                Debug.Log("Executed ActionNode MoveRandom");
                movement = boid.GetComponent<BoidMovement>();
                movement.ApplyMovement();
                break;
            default:
                break;
        }
    }
}

public enum TypeAction
{
    Arrive,
    Evade,
    Flocking,
    MoveRandom
}

