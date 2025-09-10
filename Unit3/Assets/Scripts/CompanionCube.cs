using UnityEngine;

public class CompanionCube : MonoBehaviour, IPickupable
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnDropped()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPickedUp(Transform attachPoint)
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);

        rb.isKinematic=true;
        rb.useGravity=false;
    }
}
