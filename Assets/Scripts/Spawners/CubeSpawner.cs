using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    
    protected override void Start()
    {
        _objectName = "Кубы";
        base.Start();
    }
    
    protected override void Created(Cube cubeInstance)
    {
        base.Created(cubeInstance);
        cubeInstance.Initialize(_minLifetime, _maxLifetime);
        cubeInstance.LifeEnded += CubeLifeEnded;
        cubeInstance.BombRequested += OnBombRequested;
    }
    
    private void CubeLifeEnded(Cube cube)
    {
        cube.LifeEnded -= CubeLifeEnded;
        cube.BombRequested -= OnBombRequested;
        _pool.ReturnObject(cube);
    }
    
    private void OnBombRequested(Vector3 position)
    {
        _bombSpawner.SpawnAt(position);
    }
}