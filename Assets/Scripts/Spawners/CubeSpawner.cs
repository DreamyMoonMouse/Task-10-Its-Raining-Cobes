using UnityEngine;
using System.Collections;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    
    protected override void Start()
    {
        ObjectNameValue = "Кубы";
        base.Start();
        StartCoroutine(SpawnRoutine());
    }
    
    protected override void Created(Cube cubeInstance)
    {
        base.Created(cubeInstance);
        cubeInstance.Initialize(MinLifetime, MaxLifetime);
        cubeInstance.LifeEnded += CubeLifeEnded;
        cubeInstance.BombRequested += OnBombRequested;
    }
    
    protected override IEnumerator SpawnRoutine()
    {
        bool isRunning = true;
        
        while (isRunning)
        {
            var cube = Pool.GetObject();
            cube.transform.position = GetRandomPosition();
            TotalSpawnedCountValue++;
            NotifyStatsChanged();
            
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
    
    private void CubeLifeEnded(Cube cube)
    {
        cube.LifeEnded -= CubeLifeEnded;
        _bombSpawner.SpawnAt(cube.transform.position); 
        Pool.ReturnObject(cube);
    }
    
    private void OnBombRequested(Vector3 position)
    {
        _bombSpawner.SpawnAt(position);
    }
}