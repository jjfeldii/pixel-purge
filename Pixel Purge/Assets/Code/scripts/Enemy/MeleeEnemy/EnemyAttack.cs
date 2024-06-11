using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            HealthController healthController = other.gameObject.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(_damageAmount);
            }
        }
    }
}


