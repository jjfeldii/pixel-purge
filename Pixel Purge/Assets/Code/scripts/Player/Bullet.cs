using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageAmount { get; set; }

    private void Update()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMovement>())
        {
            HealthController healthController = other.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
        else if (other.GetComponent<PlayerController>())
        {
            HealthController healthController = other.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
