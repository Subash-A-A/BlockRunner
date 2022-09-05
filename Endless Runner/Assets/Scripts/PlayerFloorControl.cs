using UnityEngine;

public class PlayerFloorControl : MonoBehaviour
{
    private bool rotateFloorLeft;
    private bool rotateFloorRight;
    private bool isShifting;
    private FloorParent floorParent;
    private PlayerMovement pm;

    private void Start()
    {
        floorParent = FindObjectOfType<FloorParent>();
        pm = GetComponent<PlayerMovement>();
        rotateFloorLeft = false;
        rotateFloorRight = false;
    }

    private void Update()
    {
        MyInput();
        RotateFloor();
    }
    void RotateFloor()
    {
        if (rotateFloorRight && !pm.isGrounded)
        {
            StartCoroutine(floorParent.DoRotation(-1));
        }
        else if (rotateFloorLeft && !pm.isGrounded)
        {
            StartCoroutine(floorParent.DoRotation(1));
        }
    }
    void MyInput()
    {
        isShifting = Input.GetKey(KeyCode.LeftShift);
        rotateFloorLeft = Input.GetKeyDown(KeyCode.Mouse0) && isShifting;
        rotateFloorRight = Input.GetKeyDown(KeyCode.Mouse1) && isShifting;
    }
}
