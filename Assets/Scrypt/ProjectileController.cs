using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    public int damage;
    public TowerController.TowerType damageType; // Tipo de daño
    public float speed = 10f; // Definir la variable speed

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage, damageType); // Pasar el tipo de daño
        }
        Destroy(gameObject);
    }
}

