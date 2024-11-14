using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    
    private int poolSize = 20;
    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    
    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            
            if (cube.TryGetComponent(out Cube cubeScript))
            {
                cubeScript.SetPool(this);
            }
            
            cube.SetActive(false);
            poolQueue.Enqueue(cube);
        }
    }
    
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject cube = poolQueue.Dequeue();
            cube.SetActive(true);
            return cube;
        }
        else
        {
            GameObject cube = Instantiate(cubePrefab);
            
            if (cube.TryGetComponent(out Cube cubeScript))
            {
                cubeScript.SetPool(this);
            }
            return cube;
        }
    }
    
    public void ReturnObject(GameObject cube)
    {
        cube.SetActive(false); 
        poolQueue.Enqueue(cube);
    }
}

