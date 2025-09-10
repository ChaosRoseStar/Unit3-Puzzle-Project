using UnityEngine;

public abstract class Command
{
    protected CompanionController companionController;

    public void  SetCompanionController(CompanionController companionController)
    {
        this.companionController = companionController;
    }

    public abstract bool IsCommandComplete();
    public abstract void Execute();
    public abstract void Cancel();
}
