using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector3 DirectionToPlayer { get; private set; }

    public float DistanceToPlayer { get; private set; }

    [SerializeField] private float _playerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (_player == null)
        {
            AwareOfPlayer = false;
            return;
        }

        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        DistanceToPlayer = enemyToPlayerVector.magnitude;

        if (DistanceToPlayer <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}

