using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigodiego : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    private int wavepointIndex = 0;
    void Start () 
    {
        target = Waypoints.points[0];
    }
    void Update() 
    {
        Vector 3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

}
