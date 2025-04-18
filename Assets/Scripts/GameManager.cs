using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public List<Boid> totalBoids = new List<Boid>();
   public List<Food> totalFood = new List<Food>();
   public Hunter hunter;


    [Range(0f, 1f)]public float weightSeparation;
    [Range(0f, 1f)]public float weightCohesion;
    [Range(0f, 1f)]public float weightAligment;

    private void Awake()
    {
        Instance = this;
    }
}


