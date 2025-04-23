using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxFoodCount = 10;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(20f, 20f);
    [SerializeField] private Transform floorTransform;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log($"Food count: {GameManager.Instance.totalFood.Count}");
        if (timer >= spawnInterval && GameManager.Instance.totalFood.Count < maxFoodCount)
        {
            SpawnFood();
            timer = 0f;
        }
    }

    void SpawnFood()
    {
        Vector3 spawnPos = GetRandomPointOnFloor();
        GameObject food = Instantiate(foodPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetRandomPointOnFloor()
    {
        Vector3 floorPos = floorTransform.position;
        float x = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float z = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);
        return new Vector3(floorPos.x + x, 0f, floorPos.z + z);
    }
}