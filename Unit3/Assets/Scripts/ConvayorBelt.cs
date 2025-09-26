using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Conveyor Settings")]
    public float speed = 2f;
    public Vector3 direction = Vector3.forward;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ColoredCube>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
            }
        }
    }
}
