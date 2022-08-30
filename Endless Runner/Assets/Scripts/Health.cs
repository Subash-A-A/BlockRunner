using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    
    private float currentHealth;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {   
            rb.velocity = Vector3.zero;
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            Time.timeScale = 0f;
        }
    }
}
