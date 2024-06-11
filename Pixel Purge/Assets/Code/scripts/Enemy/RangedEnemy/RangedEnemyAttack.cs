using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _timeBetweenShots;

    [SerializeField]
    private float _projectileDamage;

    [SerializeField]
    private float _spawnOffset = 1.0f;

    private PlayerAwarenessController _playerAwarenessController;
    private Rigidbody2D _rigidbody;
    private float _lastFireTime;

    private void Awake()
    {
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots && _rigidbody.velocity.magnitude < 0.01f)
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

        Vector3 spawnPosition = transform.position + (Vector3)(directionToPlayer.normalized * _spawnOffset);

        GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, projectileRotation);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _projectileSpeed * directionToPlayer;

        Bullet bulletScript = projectile.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damageAmount = _projectileDamage;
        }
    }
}
