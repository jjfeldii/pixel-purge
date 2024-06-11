using UnityEngine;

public class EliteEnemyAttack : MonoBehaviour
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
    private Transform _playerTransform;
    private float _lastFireTime;
    private EnemyMovement _enemyMovement;
    private void Awake()
    {
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            Vector2 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            _rigidbody.velocity = directionToPlayer * _enemyMovement.speed;

            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireProjectile(directionToPlayer);
                _lastFireTime = Time.time;
            }
        }
    }

    private void FireProjectile(Vector2 direction)
    {
        Quaternion projectileRotation = Quaternion.LookRotation(Vector3.forward, direction);
        Vector3 spawnPosition = transform.position + (Vector3)(direction.normalized * _spawnOffset);

        GameObject projectile = Instantiate(_projectilePrefab, spawnPosition, projectileRotation);
        Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = _projectileSpeed * direction;

        Bullet bulletScript = projectile.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damageAmount = _projectileDamage;
        }
    }
}
