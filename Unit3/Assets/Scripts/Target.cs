using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    public UnityEvent OnTargetHit;

    [Header("Cooldown Settings")]
    public float cooldownTime = 3f;
    public bool isOnCooldown = false;

    private float cooldownTimer = 0f;

    public void HitTarget()
    {
        if (!isOnCooldown)
        {
            OnTargetHit.Invoke();
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = cooldownTime;
    }

    private void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                cooldownTimer = 0f;
            }
        }
    }
}

