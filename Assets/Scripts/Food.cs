using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.totalFood.Add(this);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.totalFood.Remove(this);
    }
}
