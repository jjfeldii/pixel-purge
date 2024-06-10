using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Collision detected");

        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("PlayerController found");

            HealthController healthController = other.gameObject.GetComponent<HealthController>();
            if (healthController != null)
            {
                Debug.Log("HealthController found, applying damage");
                healthController.TakeDamage(_damageAmount);
            }
            else
            {
                Debug.Log("HealthController not found on collided object");
            }
        }
        else
        {
            Debug.Log("PlayerController not found on collided object");
        }
    }
}


