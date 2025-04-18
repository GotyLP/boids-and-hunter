using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPerception : MonoBehaviour
{
    [SerializeField] float _radiusAlign;
    [SerializeField] float _radiusSeparation;
    [SerializeField] float _radiusFoodDetection;
    [SerializeField] float _radiusHunterDetection;
    public List<Boid> GetBoidsInRadius(float radius)
    {
        var allBoids = GameManager.Instance.totalBoids;
        var nearby = new List<Boid>();

        foreach (var boid in allBoids)
        {
            var self = GetComponent<Boid>();
            if (boid == self) continue;

            if (Vector3.Distance(transform.position, boid.transform.position) <= radius)
                nearby.Add(boid);
        }

        return nearby;
    }

    public List<Food> GetFoodInRadius()
    {
        var allFood = GameManager.Instance.totalFood;
        var nearby = new List<Food>();

        foreach (var food in allFood)
        {
            if (food == GetComponent<Food>()) continue;
            if (Vector3.Distance(transform.position, food.transform.position) <= _radiusFoodDetection)
                nearby.Add(food);
        }

        return nearby;
    }

    public bool IsBoidNear()
    {
        var allBoids = GameManager.Instance.totalBoids;

        foreach (var boid in allBoids)
        {
            var self = GetComponent<Boid>();
            if (boid == self) continue;
            if (Vector3.Distance(transform.position, boid.transform.position) <= _radiusAlign)
                return true;
        }

        return false;
    }

    public bool IsHunterNear()
    {
        var hunter = GameManager.Instance.hunter;

        if (Vector3.Distance(transform.position, hunter.transform.position) <= _radiusHunterDetection)
        { 
            return true; 
        } else
        {
            return false;
        }
           
    }

    public bool IsFoodNear()
    {
        var allFood = GameManager.Instance.totalFood;

        foreach (var food in allFood)
        {
            if (food == GetComponent<Food>()) continue;
            if (Vector3.Distance(transform.position, food.transform.position) <= _radiusFoodDetection)
                return true;
        }

        return false;
    }

    public float AlignRadius => _radiusAlign;
    public float SeparationRadius => _radiusSeparation;
}

