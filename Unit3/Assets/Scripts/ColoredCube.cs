using UnityEngine;

public class ColoredCube : MonoBehaviour, IPickupable
{
    [Header("Cube Color")]
    public CubeColor cubeColor = CubeColor.Red;

    

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //// Auto-assign renderer if not set
        //if (cubeRenderer == null)
        //{
        //    cubeRenderer = GetComponent<MeshRenderer>();
        //}

        // Set the cube to the appropriate layer so pressure pads can detect it
        gameObject.layer = LayerMask.NameToLayer("PickupLayer");
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

        rb.isKinematic = true;
        rb.useGravity = false;
    }

    // Method to change cube color dynamically (useful for testing)
    public void SetColor(CubeColor newColor)
    {
        cubeColor = newColor;        
    }    
}
