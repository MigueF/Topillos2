using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    public GameObject[] entries;
    public GameObject bestiary;

    public void EnableEntry(int id)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[id].SetActive(false);

        }
        entries[id].SetActive(true);
    }
}
