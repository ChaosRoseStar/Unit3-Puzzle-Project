using UnityEngine;

[RequireComponent(typeof(GravityAbility))]
public class JumpAbility : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;

    public void Jump()
    {
        GravityAbility gravity = GetComponent<GravityAbility>();

        if (gravity.IsGrounded())
        {
            gravity.AddForce(Vector3.up * jumpForce);
        }
    }
}
