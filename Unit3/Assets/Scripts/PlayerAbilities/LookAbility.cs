using UnityEngine;

public class LookAbility : MonoBehaviour
{
    [SerializeField] private Camera head;
    public void Look(Vector3 lookDirection)
    { 

        head.transform.localRotation = Quaternion.Euler(-lookDirection.y, 0, 0);
        transform.rotation = Quaternion.Euler(0, lookDirection.x, 0);
    }
}
