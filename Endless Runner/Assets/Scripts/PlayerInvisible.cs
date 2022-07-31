using UnityEngine;
using System.Collections;

public class PlayerInvisible : MonoBehaviour
{
    [SerializeField] LayerMask Obstacle;

    private bool isInvisible;
    private Material playerMaterial;

    private void Start()
    {
        isInvisible = false;
        playerMaterial = GetComponent<Material>();
    }

    private void Update()
    {
        MyInput();
        IgnoreCollision();
    }

    void IgnoreCollision()
    {
        if(Physics.Raycast(transform.position, Vector3.forward,out RaycastHit hitInfo ,10f, Obstacle))
        {
            if(hitInfo.transform.tag == "Obstacle" && isInvisible)
            {
                hitInfo.collider.enabled = false;
            }
        }
    }

    void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(BecomeInvisible());
        }
    }
    
    IEnumerator BecomeInvisible()
    {
        isInvisible = true;
        yield return new WaitForSeconds(3f);
        isInvisible = false;
    }

}
