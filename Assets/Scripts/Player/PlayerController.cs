using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float MouseSens;
    [SerializeField] private float InteractDistance;

    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    [Header("GameObjects")]
    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject HeadSocket;

    // flags
    private bool bWalking;
    private bool bHasInteract;

    // physics
    private Vector3 velocity;

    // input
    private Vector2 movementInput;
    private Vector2 mouseInput;

    // interaction
    private RaycastHit forwardHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        bWalking = false;
        bHasInteract = false;

        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        Movement();
        Look();
        UpdateStates();
        CastForwardRay();
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("bWalking", bWalking);
    }

    private void GetPlayerInput()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");

        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Movement()
    {
        Vector3 forwardMovement = transform.forward * movementInput.y * MovementSpeed * Time.deltaTime;
        Vector3 rightMovement = transform.right * movementInput.x * MovementSpeed * Time.deltaTime;

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime * Time.deltaTime;

        characterController.Move(forwardMovement);
        characterController.Move(rightMovement);
        characterController.Move(velocity);
    }

    private void Look()
    {
        transform.Rotate(0, mouseInput.x * MouseSens, 0);
        Head.transform.Rotate(-mouseInput.y * MouseSens, 0, 0);

        Head.transform.position = HeadSocket.transform.position;
    }

    private void UpdateStates()
    {
        bWalking = movementInput != Vector2.zero;
    }

    private void Interact()
    {
        if (forwardHit.transform != null)
        {
            GameObject target = forwardHit.transform.gameObject;
            if (target.TryGetComponent(out IUsableObject usableObject))
            {
                usableObject.Interact();
            }
        }
    }

    private void CastForwardRay()
    {
        Vector3 from = Head.transform.position;
        Vector3 to = Head.transform.TransformDirection(Vector3.forward);

        int layerMask = 1 << 3;

        Physics.Raycast(from, to, out forwardHit, InteractDistance, layerMask);

        if (forwardHit.transform != null)
        {
            GameObject target = forwardHit.transform.gameObject;
            if (target.TryGetComponent(out IUsableObject usableObject))
            {
                bHasInteract = true;
            }
            else
            {
                bHasInteract = false;
            }
        }
        else
        {
            bHasInteract = false;
        }
    }
}