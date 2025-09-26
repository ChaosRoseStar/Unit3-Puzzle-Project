using UnityEngine;

public enum CubeColor
{
    None,
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Orange
}

public class ColorCodedPressurePad : MonoBehaviour
{
    [Header("Color Requirements")]
    [SerializeField] private CubeColor requiredColor = CubeColor.Red;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private LayerMask cubeLayer;

    private bool correctCubeOnPad = false;

    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private MeshRenderer doorLight;
    [SerializeField] private Material doorOnMat;
    [SerializeField] private Material doorOffMat;

    
    public CubeColor RequiredColor => requiredColor;
    public bool IsCorrectCubeOnPad => correctCubeOnPad;


    private void Start()
    {
        //// Set the pressure pad to show the required color
        //if (pressurePadRenderer != null && correctColorMaterial != null)
        //{
        //    pressurePadRenderer.material = correctColorMaterial;
        //}
    }

    private void Update()
    {
        Collider[] results = new Collider[10];
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, results, cubeLayer);

        bool foundCorrectCube = false;

        foreach (Collider col in results)
        {
            if (col == null) continue;

            // Check if this cube has the correct color
            ColoredCube cubeColor = col.GetComponent<ColoredCube>();
            if (cubeColor != null && cubeColor.cubeColor == requiredColor)
            {
                foundCorrectCube = true;
                break;
            }
        }

        if (foundCorrectCube && !correctCubeOnPad)
        {
            correctCubeOnPad = true;
            CorrectCubePlaced();
        }
        else if (!foundCorrectCube && correctCubeOnPad)
        {
            correctCubeOnPad = false;
            CorrectCubeRemoved();
        }
    }

    private void CorrectCubePlaced()
    {
        if (doorLight != null && doorOnMat != null)
        {
            doorLight.material = doorOnMat;
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("DoorOpen", true);
        }

        Debug.Log($"Correct {requiredColor} cube placed! Door opening...");
    }

    private void CorrectCubeRemoved()
    {
        if (doorLight != null && doorOffMat != null)
        {
            doorLight.material = doorOffMat;
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("DoorOpen", false);
        }

        Debug.Log($"{requiredColor} cube removed! Door closing...");
    }

}
