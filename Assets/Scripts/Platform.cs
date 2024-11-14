using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    private void Start()
    {
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                platform.tag = "Platform";
            }
        }
    }
}