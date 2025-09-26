using UnityEngine;

public class BlasterPickup : MonoBehaviour, ISelectable
{
    [Header("Visual Feedback")]
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material defaultMaterial;

    private MeshRenderer meshRenderer;
    private bool isPickedUp = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Set the object to the SelectionLayer so it can be interacted with
        gameObject.layer = LayerMask.NameToLayer("SelectionLayer");
    }

    public void OnHoverEnter()
    {
        if (!isPickedUp && hoverMaterial != null)
        {
            meshRenderer.material = hoverMaterial;
        }
    }

    public void OnHoverExit()
    {
        if (!isPickedUp && defaultMaterial != null)
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    public void OnSelect()
    {
        if (!isPickedUp)
        {
            PickupBlaster();
        }
    }

    private void PickupBlaster()
    {
        isPickedUp = true;

        // Find the player
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            // Enable shooting components
            ShootAbility shootAbility = playerInput.GetComponent<ShootAbility>();
            if (shootAbility != null)
            {
                shootAbility.enabled = true;
                Debug.Log("ShootAbility enabled: " + shootAbility.enabled);
            }

            // Enable the visual blaster on the player
            Transform playerBlaster = playerInput.transform.Find("Main Camera/blasterQ");
            if (playerBlaster != null)
            {
                playerBlaster.gameObject.SetActive(true);
                Debug.Log("Blaster visual enabled");
            }
        }

        Debug.Log("Blaster acquired! You can now shoot!");

        // Hide or destroy the pickup
        gameObject.SetActive(false);
    }

}
