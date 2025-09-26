using UnityEngine;

public class CubeDropper : MonoBehaviour
{
    [Header("Cube Spawning")]
    public GameObject cubePrefab;
    public Transform spawnPoint;

    [Header("Spawn Settings")]
    public float spawnForce = 0f;
    public Vector3 spawnDirection = Vector3.down;

    public void DropCube()
    {
        if (cubePrefab != null && spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.position;
            Quaternion spawnRotation = spawnPoint.rotation;

            GameObject spawnedCube = Instantiate(cubePrefab, spawnPosition, spawnRotation);

            Rigidbody cubeRigidbody = spawnedCube.GetComponent<Rigidbody>();
            if (cubeRigidbody != null && spawnForce > 0)
            {
                cubeRigidbody.AddForce(spawnDirection.normalized * spawnForce, ForceMode.Impulse);
            }
        }
    }
}
