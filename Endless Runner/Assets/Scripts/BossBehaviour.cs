using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour
{
    [Header("Boss Parameters")]
    [SerializeField] float spawnOffset = 60f;
    [SerializeField] float positionLag = 2f;
    [SerializeField] int maxHealth = 6;
    [SerializeField] int maxLife = 5;
    [SerializeField] float bossDisarmDuration = 5f;

    [Header("Gatling Gun")]
    [SerializeField] bool hasGatlingGun;
    [SerializeField] Transform gatlingGunTransform;
    [SerializeField] float gunRadius = 20f;
    [SerializeField] LayerMask target;
    [SerializeField] float aimLag = 5f;

    private Transform player;
    private BossManager bm;
    private Vector3 bossPos;
    private Animator anim;
    private Vector3 gatlingAimTarget;

    private int currentHealth;
    private int lifeLeft;

    public bool isDisarmed = false;


    private void Start()
    {
        bm = FindObjectOfType<BossManager>();
        SetPlayerTransform(bm.Player);

        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        lifeLeft = maxLife;
        isDisarmed = false;

        gatlingAimTarget = player.position;
    }

    private void Update()
    {
        BossPositionLerper();

        anim.SetBool("loadWeapon", !isDisarmed);

        if (hasGatlingGun)
        {
            GatlingGun();
        }
    }

    void BossPositionLerper()
    {
        float lag = (isDisarmed) ? positionLag - 0.5f : positionLag;
        bossPos = new Vector3(0f, 12f, player.position.z + spawnOffset);
        transform.position = Vector3.Lerp(transform.position, bossPos, lag * Time.deltaTime);
    }

    void SetPlayerTransform(Transform transform)
    {
        player = transform;
    }
    
    void GatlingGun()
    {
        if (Physics.CheckSphere(gatlingGunTransform.position, gunRadius, target))
        {
            gatlingAimTarget = Vector3.Lerp(gatlingAimTarget, player.position, aimLag * Time.deltaTime);
            gatlingGunTransform.LookAt(gatlingAimTarget, Vector3.up);  
        }
    }

    public void TakeDamage(int damage)
    {
        
        if(lifeLeft == 0)
        {
            bm.KillBoss();
        }
        else if (isDisarmed)
        {
            Debug.Log("Immune");
        }
        else if(currentHealth <= 0)
        {
            lifeLeft -= 1;
            currentHealth = maxHealth;
            StartCoroutine(DisarmBoss());
        }
        else
        {
            currentHealth -= damage;
        }
    }

    IEnumerator DisarmBoss()
    {
        isDisarmed = true;
        yield return new WaitForSeconds(bossDisarmDuration);
        isDisarmed = false;
    }

}
