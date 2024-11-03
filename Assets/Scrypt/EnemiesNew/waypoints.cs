using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waypoints : MonoBehaviour
{
    public static Dictionary<string, Transform[]> paths = new Dictionary<string, Transform[]>();
    
    void Awake()
    {
        foreach (Transform path in transform)
        {

            Transform[] points = new Transform[path.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = path.GetChild(i);
            }
            paths.Add(path.name, points);
            Debug.Log("Path added: " + path.name + " with " + points.Length + " points.");
        }
    }
}   