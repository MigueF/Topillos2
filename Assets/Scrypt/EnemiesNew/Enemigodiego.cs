using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigodiego : MonoBehaviour
{
    public float speed = 1f;
    private Transform target;
    private int wavepointIndex = 0;
    void Start () 
    {
        target = waypoints.points[0];
        
    }
    void Update() 
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            
        }
        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }
}
