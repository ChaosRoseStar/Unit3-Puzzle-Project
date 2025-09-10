using UnityEngine;

public class DoorPressurePad : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 0.5f;
    [SerializeField] private LayerMask cubeLayer;

    private bool cubeOnPad = false;

    [Header("Door Settings")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private MeshRenderer doorLight;
    [SerializeField] private Material doorOnMat;
    [SerializeField] private Material doorOffMat;

    private void Update()
    {
        //Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, cubeLayer);

        Collider[] results = new Collider[10];
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, results, cubeLayer);

        bool foundCube = false;

        foreach(Collider col in results)
        {
            if (col == null) continue;

            foundCube = true;
            break;

            //if (col.GetComponent<CompanionCube>() != null)
            //{
            //    foundCube = true;
            //    break;
            //}
        }


        if(foundCube && !cubeOnPad)
        {
            cubeOnPad = true;
            CubePlaced();
        }else if (!foundCube && cubeOnPad)
        {
            cubeOnPad = false;
            CubeRemoved();
        }
    }


    private void CubePlaced()
    {
        doorLight.material = doorOnMat;
        doorAnimator.SetBool("DoorOpen", true);
    }

    private void CubeRemoved()
    {
        doorLight.material = doorOffMat;
        doorAnimator.SetBool("DoorOpen", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
