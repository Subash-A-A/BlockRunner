using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossBehaviour : MonoBehaviour
{
    [Header("Boss Parameters")]
    [SerializeField] float spawnOffset = 60f;
    [SerializeField] float positionLag = 2f;
    [SerializeField] int maxHealth = 6;
    [SerializeField] int maxLife = 5;
    [SerializeField] float bossDisarmDuration = 5f;

    [Header("Shield")]
    [SerializeField] Material disarmShield;
    [SerializeField] Color shiedEnable;
    [SerializeField] Color shiedDisable;

    [Header("Gatling Gun")]
    [SerializeField] bool hasGatlingGun;
    [SerializeField] Transform gatlingGunTransform;
    [SerializeField] float targetLockDuration = 2f;
    [SerializeField] float gunRadius = 20f;
    [SerializeField] LayerMask target;
    [SerializeField] float aimLag = 5f;
    [SerializeField] float gunDamage = 50;

    [Header("Health Bar UI")]
    [SerializeField] Image healthBar;
    [SerializeField] GameObject lifePrefab;
    [SerializeField] Transform lifePanel;

    [HideInInspector]
    public bool isDisarmed = false;

    private Transform player;
    private BossManager bm;
    private Vector3 bossPos;
    private Animator anim;
    private Vector3 gatlingAimTarget;
    private LineRenderer lr;

    private int currentHealth;
    private int lifeLeft;

    private bool isPlayerTracked;
    private float currentLockDuration;
    private Health playerHealth;

    private void Start()
    {
        bm = FindObjectOfType<BossManager>();
        SetPlayerTransform(bm.Player);

        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        playerHealth = player.gameObject.GetComponent<Health>();

        currentHealth = maxHealth;
        lifeLeft = maxLife;
        
        isDisarmed = false;
        disarmShield.color = shiedDisable;

        gatlingAimTarget = player.position;
        healthBar.fillAmount = 1f;

        for(int i = 0; i < maxLife; i++)
        {
            Instantiate(lifePrefab, lifePanel);
        }
    }

    private void Update()
    {
        BossPositionLerper();
        HealthLerper();
        DisarmShield();

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

    void DisarmShield()
    {
        if (isDisarmed)
        {
            disarmShield.color = Color.Lerp(disarmShield.color, shiedEnable, 10 * Time.deltaTime);
        }
        else
        {
            disarmShield.color = Color.Lerp(disarmShield.color, shiedDisable, 10 * Time.deltaTime);
        }
    }
    
    void GatlingGun()
    {
        GatlingLock();
        if (Physics.CheckSphere(gatlingGunTransform.position, gunRadius, target) && !isDisarmed)
        {
            lr.enabled = true;
            gatlingAimTarget = Vector3.Lerp(gatlingAimTarget, player.position, aimLag * Time.deltaTime);
            gatlingAimTarget.z = player.position.z;

            gatlingGunTransform.LookAt(gatlingAimTarget, Vector3.up);

            lr.SetPosition(0, gatlingGunTransform.position);
            lr.SetPosition(1, gatlingAimTarget);
        }
        else
        {
            lr.enabled = false;
        }
    }

    void GatlingLock()
    {
        isPlayerTracked = Physics.Raycast(gatlingGunTransform.position, gatlingGunTransform.forward, gunRadius, target);

        if(currentLockDuration >= targetLockDuration)
        {
            Shoot();
        }

        if (isPlayerTracked)
        {
            currentLockDuration += Time.deltaTime;
        }
        else
        {
            currentLockDuration -= Time.deltaTime;
        }

        currentLockDuration = Mathf.Clamp(currentLockDuration, 0f, targetLockDuration);
        playerHealth.SetTargetAlert(currentLockDuration / targetLockDuration);
    }

    void Shoot()
    {
        playerHealth.TakeDamage(gunDamage);
        currentLockDuration = 0f;
    }

    public void TakeDamage(int damage, bool gotPunched)
    {   
        if(!isDisarmed || gotPunched)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damage;
            }
            if (currentHealth <= 0)
            {
                lifeLeft -= 1;
                Destroy(lifePanel.GetChild(0).gameObject);
                currentHealth = maxHealth;
                StartCoroutine(DisarmBoss());
            }
            if (lifeLeft == 0)
            {
                bm.KillBoss();
            }
        }
        else
        {
            Debug.Log("Immune");
        }
    }

    void HealthLerper()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)currentHealth / maxHealth, 10 * Time.deltaTime);
    }

    IEnumerator DisarmBoss()
    {
        isDisarmed = true;
        yield return new WaitForSeconds(bossDisarmDuration);
        isDisarmed = false;
    }

}
