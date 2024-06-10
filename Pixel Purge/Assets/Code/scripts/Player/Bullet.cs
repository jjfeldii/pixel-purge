using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 3);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            HealthController healthController = other.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            Destroy(gameObject);
        }
        if (other.tag.Equals("Obstacle")){
            Destroy(gameObject);
        }
    }
}
