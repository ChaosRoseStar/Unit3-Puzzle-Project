using UnityEngine;

public class PooledBullet : PooledObject
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDestroyable destroy))
        {
            destroy.OnCollided();


            linkedPool.ResetBullet(this);
        }else if (collision.gameObject.TryGetComponent(out Target target))
        {

        }
    }
}
