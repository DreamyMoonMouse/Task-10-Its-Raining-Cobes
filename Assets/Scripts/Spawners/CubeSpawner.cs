using UnityEngine;
using System.Collections;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private CubeStatsUI _ui;
    [SerializeField] private BombSpawner _bombSpawner;
    
    protected override void Start()
    {
        ObjectNameValue = "Кубы";
        base.Start();
        _ui.RegisterSpawner(this);
        StartCoroutine(SpawnRoutine());
    }
    
    protected override void OnCreated(Cube cubeInstance)
    {
        base.OnCreated(cubeInstance);
        cubeInstance.Initialize(MinLifetime, MaxLifetime);
        cubeInstance.LifeEnded += CubeLifeEnded;
        cubeInstance.BombRequested += OnBombRequested;
    }
    
    private IEnumerator SpawnRoutine()
    {
        bool isActive = true;
        
        while (isActive)
        {
            var cube = Pool.GetObject();
            cube.transform.position = GetRandomPosition();
            cube.Initialize(MinLifetime, MaxLifetime);
            TotalSpawnedCountValue++;
            OnSpawned();

            yield return new WaitForSeconds(SpawnInterval);
        }
    }
    
    private void CubeLifeEnded(Cube cube)
    {
        cube.LifeEnded -= CubeLifeEnded;
        cube.BombRequested -= OnBombRequested;
        Pool.ReturnObject(cube);
        OnReturned();
    }
    
    private void OnBombRequested(Vector3 position)
    {
        _bombSpawner.SpawnAt(position);
    }
}