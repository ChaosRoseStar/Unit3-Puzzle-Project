using UnityEngine;

public class InteractAbility : MonoBehaviour
{
    [Header("Interaction Variables")]
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private Camera head;

    private ISelectable currentHover;

    public void Interact()
    {
        Ray ray = head.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, selectableLayer))
        {
            ISelectable sel = hit.collider.GetComponent<ISelectable>();

            if (sel != null)
            {
                if (currentHover != sel)
                {
                    currentHover?.OnHoverExit();
                    sel.OnHoverEnter();
                    currentHover = sel;
                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    sel.OnSelect();
                }
            }

        }
        else
        {
            if (currentHover != null)
            {
                currentHover.OnHoverExit();
                currentHover = null;
            }
        }
    }
}
