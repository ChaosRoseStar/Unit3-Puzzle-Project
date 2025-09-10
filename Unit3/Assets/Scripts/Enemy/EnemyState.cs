using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController _enemy;

    public EnemyState(EnemyController enemy)
    {
        _enemy = enemy; 
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
