using System.Collections;
using UnityEngine;

public class Veneno : MonoBehaviour
{
    public GameObject poisonPoolPrefab; // Prefab del charco venenoso
    public float animationTime = 1.5f; // Tiempo que dura la animaci�n
    public AudioClip activationSound; // Sonido de activaci�n
    public float poisonDuration = 5f; // Duraci�n del veneno
    public float poisonDamage = 10f; // Da�o por segundo

    private Animator anim;
    private bool isActivated = false;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Enemy"))
        {
            isActivated = true;
            StartCoroutine(ActivateTrap());
        }
    }

    private IEnumerator ActivateTrap()
    {
        anim.SetTrigger("Activate"); // Dispara la animaci�n
        if (audioSource && activationSound)
        {
            audioSource.PlayOneShot(activationSound);
        }
        yield return new WaitForSeconds(animationTime);
    }

    // M�todo que se llamar� al final de la animaci�n
    public void OnAnimationEnd()
    {
        GameObject pool = Instantiate(poisonPoolPrefab, transform.position, Quaternion.identity);
        pool.AddComponent<PoisonEffect>().Initialize(poisonDuration, poisonDamage);
        Destroy(gameObject); // Destruye la trampa despu�s de activarse
    }
}

public class PoisonEffect : MonoBehaviour
{
    private float duration;
    private float damage;

    public void Initialize(float duration, float damage)
    {
        this.duration = duration;
        this.damage = damage;
        StartCoroutine(ApplyPoisonEffect());
    }

    private IEnumerator ApplyPoisonEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(Mathf.RoundToInt(damage), TowerController.TowerType.Fire); // Pasar el tipo de da�o m�gico
                        Debug.Log("Enemigo envenenado con " + damage + " de da�o");
                    }
                }
            }
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}

