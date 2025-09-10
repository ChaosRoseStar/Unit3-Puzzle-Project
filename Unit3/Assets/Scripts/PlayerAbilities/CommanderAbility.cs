using UnityEditor.UIElements;
using UnityEngine;

public class CommanderAbility : MonoBehaviour
{
    [SerializeField] private LayerMask CompatibleWithCommands;
    private CompanionController companion;

    [SerializeField] private float commandRange = 15f;
    [SerializeField] private Camera head;
    [SerializeField] private GameObject waypointPrefab;

    private void Awake()
    {
        companion = FindFirstObjectByType<CompanionController>();
    }

    public void Command()
    {
        Ray ray = head.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, commandRange, CompatibleWithCommands))
        {
            GameObject waypoint = Instantiate(waypointPrefab, hit.point, Quaternion.identity);
            Destroy(waypoint, 0.2f);
            companion.GiveCommand(new CommandMove(hit.point));
        }
    }
}