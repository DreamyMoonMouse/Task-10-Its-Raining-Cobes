using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    public GameObject[] platforms;

    void Start()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                platform.tag = "Platform";
            }
            else
            {
                Debug.Log("Платформа не инициализирована в массиве platforms.");
            }
        }
    }
}