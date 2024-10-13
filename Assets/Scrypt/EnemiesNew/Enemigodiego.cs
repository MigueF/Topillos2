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

    public string pathName;

    void Start () 
    {
        if (waypoints.paths.TryGetValue(pathName, out currentPath))
        {
            target = currentPath[0];
        }
        else
        {
            Debug.LogError("Path not found: " + pathName);
        }

       
        
    }
    void Update() 
    {
        if (currentPath == null) return;

        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

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
   
}




