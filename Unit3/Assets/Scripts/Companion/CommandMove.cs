using UnityEngine;

public class CommandMove : Command
{
    private Vector3 target;
    public override void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        companionController.GetNavMeshAgent().SetDestination(target);
    }

    public override bool IsCommandComplete()
    {
        return Vector3.Distance(target, companionController.transform.position) < 1;
    }
    public CommandMove(Vector3 target)
    {
        this.target = target;
    }
}
