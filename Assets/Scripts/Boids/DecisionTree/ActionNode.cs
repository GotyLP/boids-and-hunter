using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public TypeAction typeAction;
    private BoidMovement movement;
    private BoidFlocking flocking;

    public override void Execute(Boid boid)
    {
        switch (typeAction)
        {
            case TypeAction.Arrive:
                Debug.Log("Executed ActionNode Arrive");

                break;
            case TypeAction.Evade:
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

