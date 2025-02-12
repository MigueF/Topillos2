using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float physicalArmor = 0f; // Armadura f�sica en porcentaje
    public float magicalArmor = 0f; // Armadura m�gica en porcentaje

    private Animator animator; // Referencia al Animator
    private Enemigodiego enemyMovement; // Referencia al componente de movimiento
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        enemyMovement = GetComponent<Enemigodiego>(); // Obtener el componente de movimiento
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
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

        // Iniciar el efecto de flash
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
            CoinManager.instance.AddCoins(10);
        }
    }

    void Die()
    {
        // Activar la animaci�n de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Desactivar el movimiento del enemigo
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        // Destruir el objeto despu�s de un peque�o retraso para permitir que la animaci�n se reproduzca
        Destroy(gameObject, 1f);
    }

    // Coroutine para manejar el efecto de flash
    IEnumerator FlashRed()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red; // Cambiar el color a rojo
            yield return new WaitForSeconds(0.1f); // Esperar un breve per�odo de tiempo
            spriteRenderer.color = originalColor; // Volver al color original
        }
    }
}

