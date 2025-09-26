using UnityEngine;
using UnityEngine.Playables;

public class StateChangeTrigger : MonoBehaviour
{
    public GameState newGameState;
    public PlayableDirector timelineDirector;
    public bool playTimelineBeforeStateChange = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playTimelineBeforeStateChange && timelineDirector != null)
            {
                // Subscribe to the timeline completion event
                timelineDirector.stopped += OnTimelineFinished;
                timelineDirector.Play();
            }
            else
            {
                // Change state immediately if no timeline
                ChangeGameState();
            }
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Unsubscribe from the event
        timelineDirector.stopped -= OnTimelineFinished;

        // Change game state after timeline completes
        ChangeGameState();
    }

    private void ChangeGameState()
    {
        GameManager.instance.ChangeState(newGameState);
        Destroy(gameObject);
    }
}
