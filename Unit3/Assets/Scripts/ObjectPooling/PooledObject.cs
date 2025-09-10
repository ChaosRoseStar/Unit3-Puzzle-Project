using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private float timeToReset = 5f;
    private float currentTimer = 0f;

    [SerializeField] private Rigidbody rb;

    public ObjectPool linkedPool { get; private set; }


    public void LinkToPool(ObjectPool pool)
    {
        linkedPool = pool;
    }

    public virtual void OnEnable()
    {
        currentTimer = 0f;
    }

    private void Update()
    {
        if(currentTimer < timeToReset)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            currentTimer = 0f;
            rb.linearVelocity = Vector3.zero;
            linkedPool.ResetBullet(this);
        }
    }

    public Rigidbody GetRigidbody()
    {
        return rb; 
    }
}
