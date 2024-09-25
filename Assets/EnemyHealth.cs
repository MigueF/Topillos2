using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        CoinManager.instance.AddCoins(5);

        if (currentHealth <= 0)
        {
            Die();
            CoinManager.instance.AddCoins(10);
        }
    }

    void Die()
    {
        // Perform death-related actions here, such as playing death animation, adding score, etc.
        Destroy(gameObject);
    }
}
