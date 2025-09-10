using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyState _currentState;

    public Transform[] patrolPoints;

    public Transform _enemyEyes;
    public float _playerCheckDistance;
    public float _checkRadius = 0.4f;

    [Space]
    public float _attackRange = 2f;
    public NavMeshAgent _agent { get; private set; }
    public Transform _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new EnemyPatrolState(this);
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_enemyEyes.position + _enemyEyes.forward * _playerCheckDistance, _checkRadius);

        Gizmos.DrawLine(_enemyEyes.position, _enemyEyes.position + _enemyEyes.forward * _playerCheckDistance);
    }
}
