using UnityEngine;
using System.Collections;

public class FloorParent : MonoBehaviour
{
    public bool canRotate = true;
    private float currentRotation = 0f;
    private float smoothRotation = 0f;
    private void Start()
    {
        canRotate = true;
        currentRotation = 0f;
        smoothRotation = 0f;
    }

    private void Update()
    {
        LerpScaleRotation();
    }
    public IEnumerator DoRotation(int direction)
    {
        canRotate = false;
        currentRotation += direction * 90f;
        yield return new WaitForSeconds(0.5f);
        canRotate = true;
    }

    void LerpScaleRotation()
    {
        smoothRotation = Mathf.Lerp(smoothRotation, currentRotation, 5 * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, 0f, smoothRotation);
    }
}
