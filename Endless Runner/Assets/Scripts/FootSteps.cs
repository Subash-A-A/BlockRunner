using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private AudioSource stepSound;
    [SerializeField] LayerMask whatIsGround;

    private void Start()
    {
        stepSound = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if(Physics.CheckSphere(transform.position, 0.25f, whatIsGround) && !stepSound.isPlaying)
        {
            stepSound.Play();
        }
    }
}
