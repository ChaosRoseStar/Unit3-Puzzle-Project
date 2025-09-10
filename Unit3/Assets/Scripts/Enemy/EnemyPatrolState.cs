using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    int patrolIndex = 0;

    public EnemyPatrolState(EnemyController enemy) : base(enemy)
    {
        // use the base constructor of the EnemyState.
        // Anything below is added after.
    }

    public override void OnStateEnter()
    {
        _enemy._agent.SetDestination(_enemy.patrolPoints[patrolIndex].position);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Patrol State");
    }

    public override void OnStateUpdate()
    {
        if (_enemy._agent.remainingDistance < 2.1f)
        {
            if (patrolIndex == _enemy.patrolPoints.Length - 1)
            {
                patrolIndex = 0;
            }
            else
            {
                patrolIndex++;
            }

            _enemy._agent.SetDestination(_enemy.patrolPoints[patrolIndex].position);
        }

        // Look for player
        if (Physics.SphereCast(_enemy._enemyEyes.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Found Player!");

                _enemy._player = hit.transform;
                _enemy._agent.SetDestination(_enemy._player.position);

                _enemy.ChangeState(new EnemyChaseState(_enemy));
            }
        }
    }
}
