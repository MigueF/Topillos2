using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    public int damage;
    public TowerController.TowerType damageType; // Tipo de daño
    public float speed = 10f; // Definir la variable speed
    public float threshold = 0.1f;  //Distancia a la que se considera que la flecha ha impactado

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

        transform.rotation = Quaternion.LookRotation(dir);
        //transform.LookAt(target);
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
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

