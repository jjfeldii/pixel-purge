using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyController : MonoBehaviour
{
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
