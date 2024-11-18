using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool pool;
    [SerializeField] private GameObject mainPlatform;
    
    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;
    private float spawnInterval = 0.3f;
    private float spawnHeight = 10f;

    private void Start()
     {
         if (mainPlatform != null && mainPlatform.TryGetComponent(out BoxCollider platformCollider))
         {
             spawnAreaMin = platformCollider.bounds.min;
             spawnAreaMax = platformCollider.bounds.max;
         }
         else
         {
             Debug.LogError("Основная платформа не назначена или у неё отсутствует BoxCollider.");
             
             return;
         }
         
         StartCoroutine(SpawnCubes());
     }

     private IEnumerator SpawnCubes()
     {
         bool isRunning = true;
         
         while (isRunning)
         {
             SpawnCube();
             
             yield return new WaitForSeconds(spawnInterval);
         }
     }

     private void SpawnCube()
     {
         Vector3 spawnPosition = new Vector3(
             Random.Range(spawnAreaMin.x, spawnAreaMax.x),
             spawnAreaMax.y + spawnHeight,
             Random.Range(spawnAreaMin.z, spawnAreaMax.z)
         );
             GameObject cube = pool.GetObject(); 
             cube.transform.position = spawnPosition;
     }
}