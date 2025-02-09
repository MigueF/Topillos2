using System.Collections;
using UnityEngine;

public class Veneno : MonoBehaviour
{
    public GameObject poisonPoolPrefab; // Prefab del charco venenoso
    public float animationTime = 1.5f; // Tiempo que dura la animación
    public AudioClip activationSound; // Sonido de activación
    public float poisonDuration = 5f; // Duración del veneno
    public float poisonDamage = 10f; // Daño por segundo

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
        anim.SetTrigger("Activate"); // Dispara la animación
        if (audioSource && activationSound)
        {
            audioSource.PlayOneShot(activationSound);
        }
        yield return new WaitForSeconds(animationTime);
        GameObject pool = Instantiate(poisonPoolPrefab, transform.position, Quaternion.identity);
        pool.AddComponent<PoisonEffect>().Initialize(poisonDuration, poisonDamage);
        Destroy(gameObject); // Destruye la trampa después de activarse
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
                    // Aquí puedes añadir código para reducir la vida del enemigo
                    Debug.Log("Enemigo envenenado con " + damage + " de daño");
                }
            }
            elapsedTime += 1f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}
