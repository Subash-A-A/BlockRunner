using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform cameraTarget;
    [SerializeField] LayerMask enemyMask;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Physics.SphereCast(cameraTarget.position, 2f, Vector3.forward, out RaycastHit hitInfo, 5f, enemyMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Punch");
                rb.velocity = Vector3.zero;
            }
        }
    }
}
