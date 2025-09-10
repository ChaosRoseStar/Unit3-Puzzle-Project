using UnityEngine;

public class MoveAbility : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 moveDirection)
    {
        Vector3 forwardMovement = moveDirection.z * transform.forward;
        Vector3 sidewaysMovement = moveDirection.x * transform.right;

        Vector3 moveVector = (forwardMovement + sidewaysMovement);

        controller.Move(moveVector * Time.deltaTime * moveSpeed);
    }
}
