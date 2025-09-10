using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    // SINGLETON PATTERN
    public static PlayerInput Instance;




    [Header("Abilities")]
    [SerializeField] private MoveAbility moveAbility;
    [SerializeField] private LookAbility lookAbility;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private ShootAbility baseShootAbility;
    [SerializeField] private InteractAbility interactAbility;
    [SerializeField] private PickupAbility pickupAbility;
    [SerializeField] private CommanderAbility commanderAbility;

    [Space]
    [Header("Look Controls")]
    [SerializeField] private float mouseSens;

    private Vector2 lookDirection;
    private Vector3 moveDirection;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        if(Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.Breifing) return;


        if (lookAbility != null)
        {
            lookDirection.x += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens;
            lookDirection.y += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens;

            float angleOnY = lookDirection.y;
            lookDirection.y = Mathf.Clamp(angleOnY, -80, 80);

            lookAbility.Look(lookDirection);
        }

        if (moveAbility != null)
        {
            moveDirection.x = Input.GetAxis("Horizontal"); // ad left/right
            moveDirection.z = Input.GetAxis("Vertical"); // ws up/down

            moveAbility.Move(moveDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpAbility.Jump();
        }

        if (pickupAbility != null)
        {
            pickupAbility.Pickup();
            if (pickupAbility.hasHeldItem) return;
        }

        if (interactAbility != null)
        {
            interactAbility.Interact();
        }

        if (Input.GetMouseButtonDown(0) && baseShootAbility != null)
        {
            baseShootAbility.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.T) && commanderAbility != null)
        {
            commanderAbility.Command();
        }
    }
}
