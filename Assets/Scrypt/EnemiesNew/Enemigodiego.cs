using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemigodiego : MonoBehaviour
{
    public float speed = 20f;
    private Transform target;
    private int wavepointIndex = 0;
    public bool isSlowed;
    private Transform[] currentPath;

    void Update()
    {
        if (currentPath == null) return;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (dir.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dir.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= currentPath.Length - 1)
        {
            GameManager.instance.ReduceLives(1);
            Destroy(gameObject);
        }
        else
        {
            wavepointIndex++;
            target = currentPath[wavepointIndex];
            Debug.Log("Next waypoint: " + target.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))
        {
            GameManager.instance.ReduceLives(1);
            Destroy(gameObject);
        }
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        currentPath = waypoints;
        wavepointIndex = 0; // Reiniciar el índice de waypoints
        if (currentPath != null && currentPath.Length > 0)
        {
            target = currentPath[0];
            Debug.Log("Waypoints set for enemy.");
        }
    }
}
