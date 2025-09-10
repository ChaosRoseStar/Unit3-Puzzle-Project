using UnityEngine;

public class AttackAbility : MonoBehaviour
{
    [SerializeField] private float _damage = 5;
    [SerializeField] private float _attackCooldown = 1;

    private bool _isAttacking;
    private float _attackTimer;

    private HealthSystem target;

    private void Update()
    {
        if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackCooldown)
            {
                Attack();
                _attackTimer = 0;
            }
        }
    }

    public void Attack()
    {
        if(target)
        {
            target.DecreaseHealth(_damage);
        }
    }


    public void StartAttack(Transform _target)
    {
        target = _target.GetComponent<HealthSystem>();
        _isAttacking = true;
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }
}
