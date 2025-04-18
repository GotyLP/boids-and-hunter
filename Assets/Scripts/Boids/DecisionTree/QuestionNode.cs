using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{

    public TypeQuestion question;
    public Node trueNode;
    public Node falseNode;
    public override void Execute(Boid boid)
    {
        switch (question)
        {
            case TypeQuestion.FoodDist:
                //if (Vector3.Distance(boid.transform.position, ComidaPosition) <= boid.ComidaDetection)
                if (true)
                    trueNode.Execute(boid);
                else
                    falseNode.Execute(boid);
                break;
            case TypeQuestion.HunterDist:
                if (true)
                    trueNode.Execute(boid);
                else
                    falseNode.Execute(boid);
                break;
            case TypeQuestion.BoidPartnerDist:
                if (true)
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

