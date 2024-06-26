using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _attackRange;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }
    
    //Todo: refactor
    public float GetSpeed()
    {
        return _speed;
    }
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsDirection();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            float distanceToPlayer = _playerAwarenessController.DistanceToPlayer;

            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsDirection()
    {
        if (_targetDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, _targetDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.SetRotation(rotation);
        }
    }

    private void SetVelocity()
    {
        if (_playerAwarenessController.AwareOfPlayer && _playerAwarenessController.DistanceToPlayer > _attackRange)
        {
            float angleDifference = Vector2.Angle(transform.up, _targetDirection);
            if (angleDifference < 5.0f)
            {
                _rigidbody.velocity = transform.up * _speed;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
