using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Image healthBar;
    [SerializeField] Image secondaryHealthBar;
    [SerializeField] Image alertBar;

    private float currentHealth;
    private float targetLockAlert;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        targetLockAlert = 0f;
    }

    private void Update()
    {
        HealthLerper();
        AlertBar();
    }
    public void TakeDamage(float damage)
    {
        if(currentHealth > 0)
        {   
            rb.velocity = Vector3.zero;
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
        }
    }

    void AlertBar()
    {
        alertBar.fillAmount = targetLockAlert;
    }

    void HealthLerper()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth/maxHealth, 10 * Time.unscaledDeltaTime);
        secondaryHealthBar.fillAmount = Mathf.Lerp(secondaryHealthBar.fillAmount, currentHealth / maxHealth, 3f * Time.unscaledDeltaTime);
    }

    public void SetTargetAlert(float currentLockDuration)
    {
        targetLockAlert = currentLockDuration;
    }
}
