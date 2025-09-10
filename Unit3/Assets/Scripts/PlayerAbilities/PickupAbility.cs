using UnityEngine;

public class PickupAbility : MonoBehaviour
{
    [Header("Pickup Variables")]
    [SerializeField] private Transform attachPoint;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Camera head;
    [SerializeField] private float pickupDistance = 5f;

    private IPickupable heldObject;

    public bool hasHeldItem => heldObject != null;

    public void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                Drop();
            }
        }
    }

    private void TryPickup()
    {
        Ray ray = head.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance, pickupLayer))
        {
            IPickupable pick = hit.collider.GetComponent<IPickupable>();

            if (pick != null)
            {
                heldObject = pick;
                heldObject.OnPickedUp(attachPoint);
            }
        }
    }

    private void Drop()
    {
        heldObject.OnDropped();
        heldObject = null;
    }
}
