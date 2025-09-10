using UnityEngine;
using UnityEngine.AI;

public class NPCPathfinding : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private NavMeshAgent agent;

    private void Update()
    {
        agent.SetDestination(targetPosition.position);
    }

}
