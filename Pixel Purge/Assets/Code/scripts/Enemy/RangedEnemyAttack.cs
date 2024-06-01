using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _timeBetweenShots;

    private PlayerAwarenessController _playerAwarenessController;
    private float _lastFireTime;

    private void Awake()
    {
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void Update()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireProjectile();

                _lastFireTime = Time.time;
            }
        }
    }

    private void FireProjectile()
    {
        Vector2 directionToPlayer = _playerAwarenessController.DirectionToPlayer;
        Quaternion projectileRotation = Quaternion.LookRotation(Vector3.forward, directionToPlayer);

        GameObject projectile = Instantiate(_projectilePrefab, transform.position, projectileRotation);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _projectileSpeed * directionToPlayer;
    }
}
