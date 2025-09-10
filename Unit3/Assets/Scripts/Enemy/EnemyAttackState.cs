using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float _distanceToPlayer;
    AttackAbility _attackAbility;

    public EnemyAttackState(EnemyController enemy) : base(enemy)
    {
        _attackAbility = enemy.GetComponent<AttackAbility>();
    }

    public override void OnStateEnter()
    {
        if (_attackAbility != null)
        {
            _attackAbility.StartAttack(_enemy._player);
        }
    }

    public override void OnStateExit()
    {
        if (_attackAbility != null)
        {
            _attackAbility.StopAttack();
        }
    }

    public override void OnStateUpdate()
    {
        if (_enemy._player != null)
        {

            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);

            if (_distanceToPlayer > _enemy._attackRange)
            {
                _enemy.ChangeState(new EnemyChaseState(_enemy));
            }

            _enemy._agent.SetDestination(_enemy._player.position);
        }
        else
        {
            _enemy.ChangeState(new EnemyPatrolState(_enemy));
        }



    }
}
