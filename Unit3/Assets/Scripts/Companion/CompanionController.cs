using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private Queue<Command> commandQueue = new Queue<Command>();

    private NavMeshAgent agent;
    private Animator Anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (commandQueue.Count > 0)
        {
            Debug.Log(" Command in Queue " + commandQueue.Count);
            commandQueue.Peek().Execute();

            if(commandQueue.Peek().IsCommandComplete())
            {
                Debug.Log("Finishing Command");
                FinishCommand();
            }
        }

        Anim.SetFloat("Velocity", agent.velocity.sqrMagnitude);
    }

    public void GiveCommand(Command newCommand)
    {
        newCommand.SetCompanionController(this);
        commandQueue.Enqueue(newCommand);
    }

    public void FinishCommand()
    {
        commandQueue.Dequeue();
    }

    public NavMeshAgent GetNavMeshAgent() => agent;
}
