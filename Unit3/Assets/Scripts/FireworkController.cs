using UnityEngine;

public class FireworksController : MonoBehaviour
{
    [Header("Fireworks Settings")]
    public GameObject[] fireworkObjects;
    public float delayBetweenFireworks = 0.5f;
    public bool triggerOnStart = false;

    private void Start()
    {
        if (triggerOnStart)
        {
            TriggerFireworks();
        }

        // Listen for game state changes
        if (GameManager.instance != null)
        {
            GameManager.instance.OnStateChanged += OnGameStateChanged;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnStateChanged -= OnGameStateChanged;
        }
    }

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.GameWin)
        {
            TriggerFireworks();
        }
    }

    public void TriggerFireworks()
    {
        StartCoroutine(TriggerFireworksSequence());
    }

    private System.Collections.IEnumerator TriggerFireworksSequence()
    {
        for (int i = 0; i < fireworkObjects.Length; i++)
        {
            if (fireworkObjects[i] != null)
            {
                // Get all particle systems in the firework and its children
                ParticleSystem[] particleSystems = fireworkObjects[i].GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem ps in particleSystems)
                {
                    if (ps != null)
                    {
                        ps.Play();
                    }
                }
            }

            yield return new WaitForSeconds(delayBetweenFireworks);
        }
    }

    public void StopAllFireworks()
    {
        foreach (GameObject firework in fireworkObjects)
        {
            if (firework != null)
            {
                ParticleSystem[] particleSystems = firework.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in particleSystems)
                {
                    if (ps != null)
                    {
                        ps.Stop();
                    }
                }
            }
        }
    }
}
