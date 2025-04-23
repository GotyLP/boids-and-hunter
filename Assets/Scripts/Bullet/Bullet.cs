using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 _direction;
    public float lifetime = 5f; 
    public LayerMask wallLayerMask;

    private void Start()
    {
        Debug.Log("Bala instanciada");
        Destroy(gameObject, lifetime);
    }

    public void Init(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.TryGetComponent<Boid>(out var boid))
        {
            GameManager.Instance.RemoveBoid(boid);
            Destroy(boid.gameObject);
            Destroy(gameObject);
        }

        if (((1 << other.gameObject.layer) & wallLayerMask) != 0)
        {
            Destroy(gameObject);
        }
    }
}
