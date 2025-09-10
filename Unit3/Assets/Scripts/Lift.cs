using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform targetPosition;

    private bool isMoving = false;
    private Vector3 startPos;
    private Vector3 currentPosition;
    private Vector3 targetPos;

    private bool atStart = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleLift();
        }
    }


    public void ToggleLift()
    {
        if (isMoving) return;

        if (atStart)
        {
            targetPos = targetPosition.position;
            atStart = false;
            isMoving = true;
        }
        else
        {
            targetPos = startPos;
            atStart = true;
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speed);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }
}
