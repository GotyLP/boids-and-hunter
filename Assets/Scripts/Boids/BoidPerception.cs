using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPerception : MonoBehaviour
{
    [SerializeField] float _radiusAlign;
    [SerializeField] float _radiusSeparation;
    [SerializeField] float _radiusFoodDetection;
    [SerializeField] float _radiusTryEatFoot;
    [SerializeField] float _radiusHunterDetection;


    private Vector3 _mostNearbyFood;
    private Vector3 _hunterPosition;
    private Vector3 _hunterVelocity;


    public float AlignRadius => _radiusAlign;
    public float SeparationRadius => _radiusSeparation;
    public Vector3 MostNearbyFood => _mostNearbyFood;
    public Vector3 HunterPosition => _hunterPosition;
    public Vector3 HunterVelocity => _hunterVelocity;

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
            if (boid == null) return false;
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
            _hunterPosition = hunter.transform.position;
            _hunterVelocity = hunter.Velocity;
            return true;
        }
        else
        {
            _hunterPosition = Vector3.zero;
            _hunterVelocity = Vector3.zero;
            return false;
        }
    }

    public bool IsFoodNear()
    {
        var allFood = GameManager.Instance.totalFood;
        float minDist = float.MaxValue;
        bool found = false;

        foreach (var food in allFood)
        {
            if (food == GetComponent<Food>()) continue;

            float distance = Vector3.Distance(transform.position, food.transform.position);
            if (distance <= _radiusFoodDetection && distance < minDist)
            {
                minDist = distance;
                _mostNearbyFood = food.transform.position;
                found = true;
            }
        }

        return found;
    }

    public void TryConsumeNearbyFood()
    {
        var allFood = GameManager.Instance.totalFood;

        foreach (var food in allFood)
        {
            if (Vector3.Distance(transform.position, food.transform.position) < _radiusTryEatFoot)
            {
                food.gameObject.SetActive(false);
                allFood.Remove(food);
                break;
            }
        }
    }

}

