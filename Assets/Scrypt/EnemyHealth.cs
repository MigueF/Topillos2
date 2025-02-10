using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float physicalArmor = 0f; // Armadura f�sica en porcentaje
    public float magicalArmor = 0f; // Armadura m�gica en porcentaje

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount, TowerController.TowerType damageType)
    {
        float damageAfterArmor = damageAmount;

        if (damageType == TowerController.TowerType.Archer || damageType == TowerController.TowerType.Stone)
        {
            // Da�o f�sico
            damageAfterArmor = damageAmount * (1 - physicalArmor / 100f);
        }
        else if (damageType == TowerController.TowerType.Fire || damageType == TowerController.TowerType.Ice)
        {
            // Da�o m�gico
            damageAfterArmor = damageAmount * (1 - magicalArmor / 100f);
        }

        currentHealth -= Mathf.RoundToInt(damageAfterArmor);
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
