using UnityEngine;

public class GravityAbility : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravityAcceleration = -9.81f;
    private float currentGravity;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            currentGravity += gravityAcceleration * Time.deltaTime;
            if(currentGravity < -15f)
            {
                currentGravity = -15f;
            }
        }
        else if(currentGravity < 0)
        {
            currentGravity = 0;
        }

        Vector3 gravityVector = new Vector3();
        gravityVector.y = currentGravity;

        controller.Move(gravityVector * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.01f, groundLayer);
    }

    public void AddForce(Vector3 force)
    {
        currentGravity = force.y;
    }
}
