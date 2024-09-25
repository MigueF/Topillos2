using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform targetPosition;
    public float moveSpeed = 5f;
    public bool isSlowed;

    private Vector3 direction;

    private void Start()
    {
        targetPosition = GameObject.FindWithTag("Castle").transform;
        if(targetPosition != null)
        {
             direction = (targetPosition.position - transform.position).normalized;
        }
    }
    void Update()
    {
        if (targetPosition != null)
        {
            MoveTowardsTarget();
        }
    }

  /*  void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        Vector3 direction2 = targetPosition.position - transform.position;
        float distanceThisFrame = moveSpeed * Time.deltaTime;
        if (direction2.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
    }*/
    void MoveTowardsTarget()
    {
        //Vector3 direction = (targetPosition.position - transform.position).normalized;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void HitTarget()
    {
        Die();
    }

    void Die()
    {
        Destroy(targetPosition.gameObject);
        Destroy(gameObject);
    }
}