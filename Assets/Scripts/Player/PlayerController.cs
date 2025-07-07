using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class PlayerController : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float MouseSens;
    [SerializeField] private float InteractDistance;
    [SerializeField] private float NormalCameraFOV;
    [SerializeField] private float ZoomCameraFOV;

    [Header("Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera camera;

    [Header("GameObjects")]
    [SerializeField] private GameObject Head;
    [SerializeField] private GameObject Gun;

    // flags
    private bool bWalking;
    private bool bHasInteract;
    private bool bZoom;
    private bool bHasGun;

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
        bZoom = false;
        bHasGun = false;

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
        UpdateZoom();
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

        if (Input.mouseScrollDelta.y > 0)
        {
            bZoom = true;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            bZoom = false;
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

    private void UpdateZoom()
    {
        float fov = camera.fieldOfView;
        if (bZoom)
        {
            fov = Mathf.Lerp(fov, ZoomCameraFOV, 0.1f);
        }
        else
        {
            fov = Mathf.Lerp(fov, NormalCameraFOV, 0.1f);
        }
        camera.fieldOfView = fov;
    }

    private void Look()
    {
        transform.Rotate(0, mouseInput.x * MouseSens, 0);
        Head.transform.Rotate(-mouseInput.y * MouseSens, 0, 0);
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

    public void SetHasGun(bool bHasGun)
    {
        this.bHasGun = bHasGun;

        if (bHasGun)
        {
            Gun.SetActive(true);
        }
        else
        {
            Gun.SetActive(false);
        }
    }
}