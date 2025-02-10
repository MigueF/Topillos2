using System.Collections; // A�adir esta l�nea
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage = 10; // Da�o que inflige la mina
    private Animator animator; // Referencia al Animator
    private bool isExploding = false; // Evita m�ltiples detonaciones

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
                enemyHealth.TakeDamage(damage, TowerController.TowerType.Stone); // Aplica el da�o f�sico al enemigo
            }

            StartCoroutine(Explode()); // Inicia la explosi�n
        }
    }

    IEnumerator Explode()
    {
        isExploding = true;
        animator.SetTrigger("Explode"); // Activa la animaci�n de explosi�n

        yield return new WaitForSeconds(0.5f); // Espera el tiempo de la animaci�n

        Destroy(gameObject); // Destruye la mina despu�s de la animaci�n
    }
}
