using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;

    [SerializeField] private MeshRenderer doorLight;
    [SerializeField] private Material doorOnMat;
    [SerializeField] private Material doorOffMat;
    [SerializeField] private int doorWaitTime = 1;
    private float currentDoorWaitTime;

    private bool playerInside;



    private void OnTriggerEnter(Collider other) // Trigger when an object with a rigidbody and a collider, enter the trigger volume
    {
        if (other.CompareTag("Player"))
        {
            doorLight.material = doorOnMat;
        }
    }

    private void OnTriggerExit(Collider other) // Triggered when a rigidbody + collider exits the trigger volume
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetBool("DoorOpen", false);

            doorLight.material = doorOffMat;
            playerInside = false;
            currentDoorWaitTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void Update()
    {
        if (currentDoorWaitTime >= doorWaitTime)
        {
            doorAnimator.SetBool("DoorOpen", true);
            return;
        }

        if (playerInside)
        {
            currentDoorWaitTime += Time.deltaTime;
        }
    }

}
