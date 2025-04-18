using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public TypeAction typeAction;
    public override void Execute(Boid boid)
    {
        switch (typeAction)
        {
            case TypeAction.Arrive:
                Debug.Log("Comer");
                break;
            case TypeAction.Evade:
                Debug.Log("Evadir");
                break;
            case TypeAction.Flocking:
                Debug.Log("Walk with my homies");
                break;
            case TypeAction.MoveRandom:
                Debug.Log("Walk alone with the voices");
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

