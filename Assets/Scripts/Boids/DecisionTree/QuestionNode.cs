using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{

    public TypeQuestion question;
    public Node trueNode;
    public Node falseNode;
    private BoidPerception perception;

    public override void Execute(Boid boid)
    {
        switch (question)
        {
            case TypeQuestion.FoodDist:
                perception = boid.GetComponent<BoidPerception>();
                if (perception.IsFoodNear())
                    trueNode.Execute(boid);
                else
                    falseNode.Execute(boid);
                break;
            case TypeQuestion.HunterDist:
                perception = boid.GetComponent<BoidPerception>();
                if (perception.IsHunterNear())
                    trueNode.Execute(boid);
                else
                    falseNode.Execute(boid);
                break;
            case TypeQuestion.BoidPartnerDist:
                perception = boid.GetComponent<BoidPerception>();
                if (perception.IsBoidNear())
                    trueNode.Execute(boid);
                else
                    falseNode.Execute(boid);
                break;
            default:
                break;
        }
    }
}

public enum TypeQuestion
{
    FoodDist,
    HunterDist,
    BoidPartnerDist
}

