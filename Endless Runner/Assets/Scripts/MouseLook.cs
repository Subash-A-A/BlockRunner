using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 40f;
    [SerializeField] Transform cameraTarget;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation += horizontal;

        yRotation -= vertical;
        yRotation = Mathf.Clamp(yRotation, -30f, 30f);

        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);

        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = cameraTarget.position;
    }
}
