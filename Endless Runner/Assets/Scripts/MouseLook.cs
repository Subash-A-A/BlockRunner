using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 40f;
    [SerializeField] float smooth = 10f;
    [SerializeField] Transform camHolder;
    [SerializeField] Transform camTarget;
    [SerializeField] Transform orientation;
    [SerializeField] WeaponHolder holder;

    private float xRotation;
    private float yRotation;
    private Quaternion armRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        FollowTarget();
        RotationLerper();
    }

    void Look()
    {
        float horizontal = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation += horizontal;

        yRotation -= vertical;
        yRotation = Mathf.Clamp(yRotation, -30f, 30f);
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);


        if (holder.isHoldingWeapon())
        {
            armRotation = camHolder.localRotation;
        }
        else
        {
            armRotation = Quaternion.Euler(0f, 0f, 0f);
        }

        camHolder.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
    }

    void FollowTarget()
    {
        camHolder.position = camTarget.position; 
    }

    void RotationLerper()
    {
        orientation.localRotation = Quaternion.Lerp(orientation.localRotation, armRotation, smooth * Time.deltaTime);
    }
}
