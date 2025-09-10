using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Entered Chase State");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Chase State");
    }

    public override void OnStateUpdate()
    {
        if(_enemy._player != null)
        {
            if (Physics.Linecast(_enemy._enemyEyes.position, _enemy._player.position + (Vector3.up * 0.5f), out RaycastHit info))
            {
                if (info.transform.gameObject.layer != 3)
                {
                    ReturnToPatrol();
                }
                else
                {
                    Debug.Log(info.transform.gameObject.layer);
                    _enemy._agent.SetDestination(_enemy._player.position);
                }
            }

            float distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);


            if(distanceToPlayer < _enemy._attackRange)
            {
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            if (distanceToPlayer > 10)
            {
                ReturnToPatrol();
            }
        }
        else
        {
            ReturnToPatrol();
        }

    }

    private void ReturnToPatrol()
    {
        Debug.Log("Lost Player!");
        _enemy.ChangeState(new EnemyPatrolState(_enemy));
    }
}
