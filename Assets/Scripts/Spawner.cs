using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool pool;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    public float spawnInterval = 1.0f;

     void Start()
     {
         if (pool == null)
         {
             Debug.Log("Pool не установлен в инспекторе!");
             return;
         }

         StartCoroutine(SpawnCubes());
     }

     IEnumerator SpawnCubes()
     {
         while (true)
         {
             SpawnCube();
             yield return new WaitForSeconds(spawnInterval);
         }
     }

     void SpawnCube()
     {
         Vector3 spawnPosition = new Vector3(
             Random.Range(spawnAreaMin.x, spawnAreaMax.x),
             spawnAreaMax.y,
             Random.Range(spawnAreaMin.z, spawnAreaMax.z)
         );
         
             GameObject cube = pool.gameObject; // Получаем объект из пула
             cube.SetActive(true);
             cube.transform.position = spawnPosition;
     }
}