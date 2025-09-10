using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IDestroyable destroy))
        {
            destroy.OnCollided();
            Destroy(gameObject);
        }
    }
}
