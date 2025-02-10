using System.Collections; // Añadir esta línea
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage = 10; // Daño que inflige la mina
    private Animator animator; // Referencia al Animator
    private bool isExploding = false; // Evita múltiples detonaciones

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isExploding) return; // Evita que se active varias veces

        if (other.CompareTag("Enemy")) // Verifica si el objeto que colisiona es un enemigo
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, TowerController.TowerType.Stone); // Aplica el daño físico al enemigo
            }

            StartCoroutine(Explode()); // Inicia la explosión
        }
    }

    IEnumerator Explode()
    {
        isExploding = true;
        animator.SetTrigger("Explode"); // Activa la animación de explosión

        yield return new WaitForSeconds(0.5f); // Espera el tiempo de la animación

        Destroy(gameObject); // Destruye la mina después de la animación
    }
}
