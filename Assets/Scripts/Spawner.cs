using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private GameObject _mainPlatform;
    
    private Pool<Cube> _pool;
    private Vector3 _spawnAreaMin;
    private Vector3 _spawnAreaMax;
    private float _spawnInterval = 0.3f;
    private float _spawnHeight = 10f;
    private int _poolSize = 20;

    private void Start()
     {
         if (_mainPlatform != null && _mainPlatform.TryGetComponent(out BoxCollider platformCollider))
         {
             _spawnAreaMin = platformCollider.bounds.min;
             _spawnAreaMax = platformCollider.bounds.max;
         }
         
         _pool = new Pool<Cube>(_cubePrefab, _poolSize);
         StartCoroutine(SpawnCubes());
     }

     private IEnumerator SpawnCubes()
     {
         bool isRunning = true;
         
         while (isRunning)
         {
             SpawnCube();
             var wait = new WaitForSeconds(_spawnInterval);
             
             yield return wait;
         }
     }

     private void SpawnCube()
     {
         Cube cube = _pool.GetObject();
         cube.transform.position = GetRandomSpawnPosition();
         cube.OnLifeEnded += HandleCubeLifeEnded;
     }
     
     private void HandleCubeLifeEnded(Cube cube)
     {
         cube.OnLifeEnded -= HandleCubeLifeEnded; 
         _pool.ReturnObject(cube); 
     }

     private Vector3 GetRandomSpawnPosition()
     {
         return new Vector3(
             Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
             _spawnAreaMax.y + _spawnHeight,
             Random.Range(_spawnAreaMin.z, _spawnAreaMax.z)
         );
     }
}