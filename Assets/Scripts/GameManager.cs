using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public List<Boid> totalBoids = new List<Boid>();
   public List<Food> totalFood = new List<Food>();
   public Hunter hunter;


    [Range(0f, 1f)]public float wightSeparation;
    [Range(0f, 1f)]public float wightCohesion;
    [Range(0f, 1f)]public float wightAligment;

    private void Awake()
    {
        Instance = this;
    }
}


