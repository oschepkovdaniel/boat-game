using UnityEngine;

public class CameraRoll : MonoBehaviour
{

    [SerializeField] private Transform CameraTransform;
    [SerializeField] private float RollAngle = 5.0f;
    [SerializeField] private float RollSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetRoll = 0;
        float horizontal = Input.GetAxis("Horizontal");

        targetRoll = -horizontal * RollAngle;

        Quaternion targetRotation = Quaternion.Euler(0, 0, targetRoll);
        CameraTransform.localRotation = Quaternion.Lerp(CameraTransform.localRotation, targetRotation, Time.deltaTime * RollSpeed);
    }
}
