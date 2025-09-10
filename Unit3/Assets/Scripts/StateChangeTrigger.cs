using UnityEngine;

public class StateChangeTrigger : MonoBehaviour
{
    public GameState newGameState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ChangeState(newGameState);
            Destroy(gameObject);
        }
    }
}
